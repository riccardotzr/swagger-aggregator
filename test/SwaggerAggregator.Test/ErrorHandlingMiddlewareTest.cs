using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Validations;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SwaggerAggregator.Test
{
    [Collection("Error Handling Middleware")]
    public class ErrorHandlingMiddlewareTest
    {
        [Fact(DisplayName =  "Should return HTTP Status Code 200")]
        public async Task Should_Return_Ok()
        {
            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var context = new DefaultHttpContext();
            context.Request.Path = "/";
            context.Response.Body = new MemoryStream();

            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                innerHttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return Task.CompletedTask;
            }, logger.Object);

            await middleware.Invoke(context);

            context.Response.StatusCode.Should().Be(200);
        }

        [Fact(DisplayName = "Should return HTTP Status Code 400")]
        public async Task Should_Return_Bad_Request()
        {
            var expectedException = new ReaderException("An error has occurred while reading content from: https://my-microservice-com/")
            {
                Endpoint = "https://my-microservice-com/",
                Errors = new List<OpenApiError>
                {
                    new OpenApiError("#/paths", "The field 'paths' in 'document' object is REQUIRED.")
                }
            };

            var expectedResponse = new BadRequestResponse
            {
                Message = "Validation Errors",
                Endpoint = "https://my-microservice-com/",
                Errors = new List<ValidationError>
                {
                    new ValidationError { Message = "The field 'paths' in 'document' object is REQUIRED.", Pointer = "#/paths" }
                }
            };

            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var context = new DefaultHttpContext();
            context.Request.Path = "/";
            context.Response.Body = new MemoryStream();

            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                innerHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Task.FromException(expectedException);
            }, logger.Object);

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var body = reader.ReadToEnd();
            var actualResponse = JsonConvert.DeserializeObject<BadRequestResponse>(body);

            context.Response.StatusCode.Should().Be(400);
            context.Response.ContentType.Should().Be("application/json; charset=utf-8");
            actualResponse.Should().BeOfType<BadRequestResponse>();
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact(DisplayName = "Should return HTTP Status Code 500")]
        public async Task Should_Return_Internal_Server_Error()
        {
            var expectedException = new Exception("The operation could not be completed");

            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var context = new DefaultHttpContext();
            context.Request.Path = "/";
            context.Response.Body = new MemoryStream();

            var middleware = new ErrorHandlingMiddleware(next: (innerHttpContext) =>
            {
                innerHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Task.FromException(expectedException);
            }, logger.Object);

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var body = reader.ReadToEnd();
            var actualResponse = JsonConvert.DeserializeObject<InternalServerErrorResponse>(body);

            context.Response.StatusCode.Should().Be(500);
            context.Response.ContentType.Should().Be("application/json; charset=utf-8");
            actualResponse.Should().BeOfType<InternalServerErrorResponse>();
        }
    }
}
