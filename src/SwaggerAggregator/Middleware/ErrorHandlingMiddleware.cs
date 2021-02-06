using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Validations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation($"Start Request ${httpContext.Request.Path}");

                await _next(httpContext);

                _logger.LogInformation($"End Request ${httpContext.Request.Path} with status code: ${ httpContext.Response.StatusCode }");
            }
            catch (Exception ex)
            {
                await HandleError(httpContext, ex);
            }
        }

        private async Task HandleError(HttpContext httpContext, Exception exception)
        {
            var exceptionType = exception.GetType();

            if (exceptionType == typeof(ReaderException))
            {
                var readerException = exception as ReaderException;

                var response = new BadRequestResponse
                {
                    Message = "Validation Errors",
                    Endpoint = readerException.Endpoint,
                    Errors = readerException.Errors.Select(x => new ValidationError { Message = x.Message, Pointer = x.Pointer }).ToList()
                };

                httpContext.Response.ContentType = "application/json; charset=utf-8";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            else
            {
                var response = new InternalServerErrorResponse
                {
                    Message = exception.Message,
                    Exception = exception
                };

                httpContext.Response.ContentType = "application/json; charset=utf-8";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }

            _logger.LogInformation($"End Request ${httpContext.Request.Path} with status code: ${ httpContext.Response.StatusCode }");
        }
    }
}
