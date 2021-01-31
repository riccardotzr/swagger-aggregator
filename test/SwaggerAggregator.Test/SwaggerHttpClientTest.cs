using FluentAssertions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Validations;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SwaggerAggregator.Test
{
    [Collection("HttpClient")]
    public class SwaggerHttpClientTest
    {
        [Theory(DisplayName = "Returns HTTP Status Code 200")]
        [InlineData("Samples/MockFirstDocument.yaml")]
        [InlineData("Samples/MockSecondDocument.yaml")]
        public async Task Should_Returns_Ok(string path)
        { 
            var content = Resources.GetFileContent(path);

            var client = new HttpClient(new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content),
            }));

            var swaggerClient = new SwaggerHttpClient(client);
            var actualResult = await swaggerClient.GetOpenApiDocument("https://my-microservice.com/");

            var expectedResult = new OpenApiStringReader().Read(content, out var diagnostic);

            actualResult.Should().NotBeNull();
            diagnostic.Errors.Should().BeEmpty();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Theory(DisplayName = "Throws an exception when dependency returns an error")]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Should_Throws_An_Exception_When_Dependency_Returns_An_Error(HttpStatusCode statusCode)
        {
            var endpoint = "https://my-microservice.com/";
            
            var client = new HttpClient(new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent("An error has occurred please try again later"),
            }));

            var swaggerClient = new SwaggerHttpClient(client);
            var result = await Assert.ThrowsAsync<Exception>(() => swaggerClient.GetOpenApiDocument(endpoint));

            result.Should().NotBeNull();
            result.Should().BeOfType<Exception>();
            result.Message.Should().Be($"The operation could not be completed. Endpoint: {endpoint}");

        }

        [Fact(DisplayName = "Throws an exception when OpenApiStringReader fail due to a validation error")]
        public async Task Should_Throws_An_Exception_When_OpenApiStreamReader_Fail_Validation()
        {
            var expectedErrors = new List<OpenApiValidatorError>()
            {
                new OpenApiValidatorError("OpenApiDocumentFieldIsMissing", "#/paths", "The field 'paths' in 'document' object is REQUIRED."),
            };

            var endpoint = "https://my-microservice.com/";
            var content = Resources.GetFileContent("Samples/ValidationExceptionDocument.yaml");

            var client = new HttpClient(new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content),
            }));

            var swaggerClient = new SwaggerHttpClient(client);
            var result = await Assert.ThrowsAsync<ReaderException>(() => swaggerClient.GetOpenApiDocument(endpoint));

            result.Should().NotBeNull();
            result.Should().BeOfType<ReaderException>();
            result.Message.Should().Be($"An error has occurred while reading content from: {endpoint}");
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().BeEquivalentTo(expectedErrors);
        }
    }
}
