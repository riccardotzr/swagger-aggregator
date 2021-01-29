using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SwaggerAggregator.Test
{
    [Collection("Swagger Aggregator Provider")]
    public class SwaggerAggregatorProvidertest
    {
        [Fact]
        public void Test()
        {
            var options = new SwaggerAggregatorOptions()
            {
                Info = new Info
                {
                    Title = "Swagger Aggregator Microservices",
                    Description = "Description Swagger Aggregator Microservices"
                },
                Servers = new List<Server>
                {
                    new Server { Url = "https://staging.environment/", Description = "Microservices One Swagger" },
                },
                Services = new List<Service>
                {
                    new Service { Url = "https://microservice-one/swagger/", Name = "Microservices One", Version = "1.0.0", RemoveApiPrefix = false  },
                    new Service { Url = "https://microservice-two/swagger/", Name = "Microservices Two", Version = "1.0.0", RemoveApiPrefix = false  }
                }
            };

            var optionMock = new Mock<IOptions<SwaggerAggregatorOptions>>();
            optionMock.Setup(x => x.Value).Returns(options);

            var provider = new SwaggerAggregatorProvider(optionMock.Object, null);
            provider.GetSwagger("v1");
        }
    }
}
