
using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SwaggerAggregator.UnitTests
{
    [Collection("FileSystemService")]
    public class FileSystemServiceTest
    {
        [Theory]
        [InlineData("Utils/Samples/FileSystem/ValidOpenApiV3.yaml")]
        public async Task Should_Be_Valid_Yaml(string path) 
        {
            var expectedResult = Resources.GetYamlFile(path);
            var actualResult = await new FileSystemService().ReadFile(path);

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}