using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Validations;
using Moq;
using Xunit;

namespace SwaggerAggregator.UnitTests
{
    [Collection("SwaggerHttpClient")]
    public class SwaggerHttpClientTest
    {
        [Theory(DisplayName = "Returns HTTP Status Code 200")]
        [InlineData("Utils/Samples/SwaggerAggregator/MockAggregateDocument.yaml")]
        [InlineData("Utils/Samples/SwaggerAggregator/MockAggregateDocumentExclude.yaml")]
        [InlineData("Utils/Samples/SwaggerAggregator/MockAggregateDocumentInclude.yaml")]
        [InlineData("Utils/Samples/SwaggerAggregator/MockFirstDocument.yaml")]
        [InlineData("Utils/Samples/SwaggerAggregator/MockSecondDocument.yaml")]
        public async Task Should_Return_Ok(string path)
        {
            var content = Resources.GetFileContent(path);

            var httpClient = new HttpClient(new MockHttpMessageHandler(new HttpResponseMessage 
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content),
            }));
            
            var swaggerHttpClient = new SwaggerHttpClient(httpClient);
            var actualResult = await swaggerHttpClient.GetOpenApiDocument("https://my-microservice.com/");

            var expectedResult = new OpenApiStringReader().Read(content, out var diagnostic);

            actualResult.Should().NotBeNull();
            diagnostic.Errors.Should().BeEmpty();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
        
        [Fact(DisplayName = "Throws an exception when OpenApiStringReader fail due to a validation error")]
        public async Task Should_Throw_An_Exception_When_OpenApiStreamReader_Fail_Validation()
        {
            var endpoint = "https://my-microservice.com/";

            var expectedErrors = new List<OpenApiValidatorError>()
            {
                new OpenApiValidatorError("OpenApiDocumentFieldIsMissing", "#/paths", "The field 'paths' in 'document' object is REQUIRED."),
            };
            var content = Resources.GetFileContent("Utils/Samples/SwaggerAggregator/MockErrorDocument.yaml");

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
            result.Endpoint.Should().Be(endpoint);
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().BeEquivalentTo(expectedErrors);
        }

        [Theory(DisplayName = "Throws an exception when dependency returns an error")]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Should_Throw_An_Exception_When_Dependency_Returns_An_Error(HttpStatusCode statusCode)
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
    }
}
