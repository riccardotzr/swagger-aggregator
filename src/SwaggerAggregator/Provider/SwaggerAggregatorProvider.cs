using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerAggregatorProvider : ISwaggerProvider
    {
        private readonly SwaggerAggregatorOptions _options;
        private readonly ISwaggerHttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="client"></param>
        public SwaggerAggregatorProvider(IOptions<SwaggerAggregatorOptions> options, ISwaggerHttpClient client)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc />
        public OpenApiDocument GetSwagger(string documentName, string host = null, string basePath = null)
        {
            var services = _options.Services.Where(x => x.Version == documentName).ToList();
            var version = services.FirstOrDefault(x => x.Version == documentName)?.Version;

            var builder = new OpenApiDocumentBuilder();
            builder.SetServer(_options.Servers);
            builder.SetInfo(_options.Info, version);

            foreach (var item in services)
            {
                var currentServiceDocument = _client.GetOpenApiDocument(item.Url).GetAwaiter().GetResult();

                if (currentServiceDocument.Tags != null && currentServiceDocument.Tags.Any())
                {

                }

                if (currentServiceDocument.Paths != null)
                {
                    builder.SetPath(currentServiceDocument.Paths, item.RemoveApiPrefix);
                }

                if (currentServiceDocument.Components != null && currentServiceDocument.Components.Schemas.Any())
                {
                    builder.SetSchemas(currentServiceDocument.Components.Schemas);
                }
            }

            return builder.Build();
        }
    }
}
