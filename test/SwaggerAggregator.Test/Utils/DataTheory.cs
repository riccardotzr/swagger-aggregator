using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAggregator.Test
{
    public class SwaggerAggregatorOptionsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new SwaggerAggregatorOptions
                {
                    Info = new Info
                    {
                        Title = "Microservice Swagger Aggregator",
                        Description = "An example of Swagger Aggregator",
                    },
                    Servers = new List<Server>
                    {
                        new Server { Url = "http://localhost", Description = "Localhost server URL" },
                    },
                    Services = new List<Service>
                    {
                        new Service { Url = "https://microservice-one/swagger/", Name = "Microservices One", Version = "1.0", RemoveApiPrefix = false },
                        new Service { Url = "https://microservice-two/swagger/", Name = "Microservices Two", Version = "1.0", RemoveApiPrefix = false }
                    }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
