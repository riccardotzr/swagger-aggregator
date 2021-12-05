using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerAggregatorMiddleware
    {
        #region DYNAMIC

        #region FIELDS

        private readonly RequestDelegate _next;
        private readonly TemplateMatcher _requestMatcher;
        private readonly SwaggerAggregatorOptions _options;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SwaggerAggregatorMiddleware(RequestDelegate next, IOptions<SwaggerAggregatorOptions> options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _requestMatcher = new TemplateMatcher(TemplateParser.Parse(_options.Route), new RouteValueDictionary());
        }


        #endregion

        #region PROPERTIES
        #endregion

        #region METHODS

        #region PUBLIC

        /// <inheritdoc />
        public async Task Invoke(HttpContext httpContext, ISwaggerAggregatorService service)
        {
            var request = httpContext.Request;
            var response = httpContext.Response;

            try
            {
                var isSwaggerAggregatorRoute = IsSwaggerAggregatorRoute(request);

                if (!isSwaggerAggregatorRoute)
                {
                    await _next(httpContext);
                    return;
                }

                var swagger = await service.GetAggregateSwagger(_options.Title, _options.Description, _options.Version, _options.Services);

                if (Path.GetExtension(request.Path.Value) == ".yaml")
                {
                    await RespondWithYaml(response, swagger);
                }
                else
                {
                    await RespondWithJson(response, swagger);
                }
            }
            catch (Exception)
            {
                response.StatusCode = 500;
            }
        }

        #endregion

        #region PRIVATE

        private bool IsSwaggerAggregatorRoute(HttpRequest request)
        {
            if (request.Method != "GET")
            {
                return false;
            }

            var routeValues = new RouteValueDictionary();

            if (!_requestMatcher.TryMatch(request.Path, routeValues))
            {
                return false;
            }

            return true;
        }

        private async Task RespondWithYaml(HttpResponse response, OpenApiDocument swagger)
        {
            response.StatusCode = 200;
            response.ContentType = "text/yaml;charset=utf-8";

            using (var textWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                var yamlWriter = new OpenApiYamlWriter(textWriter);
                swagger.SerializeAsV3(yamlWriter);

                await response.WriteAsync(textWriter.ToString(), new UTF8Encoding(false));
            }
        }

        private async Task RespondWithJson(HttpResponse response, OpenApiDocument swagger)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json;charset=utf-8";

            using (var textWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                var jsonWriter = new OpenApiJsonWriter(textWriter);
                swagger.SerializeAsV3(jsonWriter);

                await response.WriteAsync(textWriter.ToString(), new UTF8Encoding(false));
            }
        }

        #endregion

        #endregion

        #endregion
    }
}