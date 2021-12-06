using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using Microsoft.OpenApi.Models;

namespace SwaggerAggregator.UnitTests
{
    [Collection("SwaggerAggregatorService")]
    public class SwaggerAggregatorServiceTest
    {
        [Theory(DisplayName = "SwaggerAggregatorService")]
        [ClassData(typeof(SwaggerAggregatorOptionsTestData))]
        public async Task Should_Be_Aggregated(SwaggerAggregatorTestData data) 
        {
            var expectedResult = Resources.GetYamlFile(data.ExpectedAggregatedResultFile);
            var httpClientResult = Resources.GetYamlFile(data.HttpClientMockFile);
            var fileSystemResult = Resources.GetYamlFile(data.FileSystemMockFile);

            var swaggerHttpClientMock = new Mock<ISwaggerHttpClient>();
            swaggerHttpClientMock.Setup(x => x.GetOpenApiDocument(It.IsAny<string>())).ReturnsAsync(httpClientResult);

            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(x => x.ReadFile(It.IsAny<string>())).ReturnsAsync(fileSystemResult);

            var swaggerAggregatorService = new SwaggerAggregatorService(swaggerHttpClientMock.Object, fileSystemServiceMock.Object);
            var actualResult = await swaggerAggregatorService.GetAggregateSwagger(data.Options.Title, data.Options.Description, data.Options.Version, data.Options.Services);

            actualResult.Should().NotBeNull();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
