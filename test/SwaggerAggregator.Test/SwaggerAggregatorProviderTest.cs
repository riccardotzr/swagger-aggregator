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
        [Theory(DisplayName = "")]
        [ClassData(typeof(SwaggerAggregatorOptionsData))]
        public void Should_Be_The_Same(SwaggerAggregatorOptions options)
        {
            var optionMock = new Mock<IOptions<SwaggerAggregatorOptions>>();
            optionMock.Setup(x => x.Value).Returns(options);

            var httpClient = new HttpClient(new MockHttpMessageHandlerSwaggerProvider());
            var swaggerClient = new SwaggerHttpClient(httpClient);

            var provider = new SwaggerAggregatorProvider(optionMock.Object, swaggerClient);
            var actual = provider.GetSwagger("1.0");

            var expectedResult = new OpenApiStreamReader().Read(Resources.GetStreamContent("Samples/AggregatorDocument.yaml"), out var diagnostic);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expectedResult);
        }
    }
}
