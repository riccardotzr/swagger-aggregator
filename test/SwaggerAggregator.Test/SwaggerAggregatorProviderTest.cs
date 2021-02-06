using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Readers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SwaggerAggregator.Test
{
    [Collection("Swagger Aggregator Provider")]
    public class SwaggerAggregatorProvidertest
    {
        [Theory(DisplayName = "Swagger Aggregator By Version")]
        [ClassData(typeof(SwaggerAggregatorOptionsData))]
        public void Should_Be_Aggregated_By_Version(SwaggerAggregatorData data)
        {
            var optionMock = new Mock<IOptions<SwaggerAggregatorOptions>>();
            optionMock.Setup(x => x.Value).Returns(data.Options);

            var httpClient = new HttpClient(new MockHttpMessageHandlerSwaggerProvider(data.HttpClientMockData));
            var swaggerClient = new SwaggerHttpClient(httpClient);

            var provider = new SwaggerAggregatorProvider(optionMock.Object, swaggerClient);
            var actual = provider.GetSwagger(data.Version);

            var expectedResult = new OpenApiStreamReader().Read(Resources.GetStreamContent(data.ExpectedResultFile), out var diagnostic);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expectedResult);
        }
    }
}
