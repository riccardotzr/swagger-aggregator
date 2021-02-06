using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerAggregator.Test
{
    public class SwaggerAggregatorData
    {
        public string Version { get; set; }

        public string ExpectedResultFile { get; set; }

        public Dictionary<string, string> HttpClientMockData { get; set; }

        public SwaggerAggregatorOptions Options { get; set; }
    }

    public class SwaggerAggregatorOptionsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Aggregation First Version Without Remove Api Prefix
            yield return new object[]
            {   
                new SwaggerAggregatorData
                {
                    Version = "1.0",
                    ExpectedResultFile = "Samples/AggregatorDocument.yaml",
                    HttpClientMockData = new Dictionary<string, string>
                    {
                        { "https://microservice-one/swagger/", "Samples/MockFirstDocument.yaml" },
                        { "https://microservice-two/swagger/", "Samples/MockSecondDocument.yaml" },
                    },
                    Options = new SwaggerAggregatorOptions
                    {
                        Info = new Info
                        {
                            Title = "Microservice Swagger Aggregator",
                            Description = "An example of Swagger Aggregator",
                            TermsOfService = "http://swagger.io/terms/",
                            Contact = new Contact
                            {
                                Email = "support@swagger.io"
                            },
                            License = new License
                            {
                                Name = "Apache 2.0",
                                Url = "http://www.apache.org/licenses/LICENSE-2.0.html"
                            }
                        },
                        Servers = new List<Server>
                        {
                            new Server { Url = "http://localhost", Description = "Localhost server URL" },
                        },
                        Services = new List<Service>
                        {
                            new Service { Url = "https://microservice-one/swagger/", Name = "Microservices One", Version = "1.0", RemoveApiPrefix = false },
                            new Service { Url = "https://microservice-two/swagger/", Name = "Microservices Two", Version = "1.0", RemoveApiPrefix = false },
                            new Service { Url = "https://microservice-one-v2/swagger", Name = "Microservices One", Version = "2.0", RemoveApiPrefix = false},
                        }
                    }
                }
            };

            // Aggregation First Version Without Remove Api Prefix
            yield return new object[]
            {
                new SwaggerAggregatorData
                {
                    Version = "2.0",
                    ExpectedResultFile = "Samples/AggregatorDocumentV2.yaml",
                    HttpClientMockData = new Dictionary<string, string>
                    {
                        { "https://microservice-one/swagger/", "Samples/MockFirstDocument.yaml" },
                        { "https://microservice-two/swagger/", "Samples/MockSecondDocument.yaml" },
                        { "https://microservice-one-v2/swagger/", "Samples/MockFirstDocumentV2.yaml" },
                        { "https://microservice-two-v2/swagger/", "Samples/MockSecondDocumentV2.yaml" }
                    },
                    Options = new SwaggerAggregatorOptions
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
                            new Service { Url = "https://microservice-two/swagger/", Name = "Microservices Two", Version = "1.0", RemoveApiPrefix = false },
                            new Service { Url = "https://microservice-one-v2/swagger/", Name = "Microservices One", Version = "2.0", RemoveApiPrefix = false},
                            new Service { Url = "https://microservice-two-v2/swagger/", Name = "Microservices Two", Version = "2.0", RemoveApiPrefix = false},
                        }
                    }
                }
            };

            // Aggregation With Remove Api Prefix
            yield return new object[]
            {
                new SwaggerAggregatorData
                {
                    Version = "1.0",
                    ExpectedResultFile = "Samples/AggregatorDocument.yaml",
                    HttpClientMockData = new Dictionary<string, string>
                    {
                        { "https://microservice-one/swagger/", "Samples/MockFirstDocumentWithApiPrefix.yaml" },
                        { "https://microservice-two/swagger/", "Samples/MockSecondDocumentWithApiPrefix.yaml" },
                    },
                    Options = new SwaggerAggregatorOptions
                    {
                        Info = new Info
                        {
                            Title = "Microservice Swagger Aggregator",
                            Description = "An example of Swagger Aggregator",
                            TermsOfService = "http://swagger.io/terms/",
                            Contact = new Contact
                            {
                                Email = "support@swagger.io"
                            },
                            License = new License
                            {
                                Name = "Apache 2.0",
                                Url = "http://www.apache.org/licenses/LICENSE-2.0.html"
                            }
                        },
                        Servers = new List<Server>
                        {
                            new Server { Url = "http://localhost", Description = "Localhost server URL" },
                        },
                        Services = new List<Service>
                        {
                            new Service { Url = "https://microservice-one/swagger/", Name = "Microservices One", Version = "1.0", RemoveApiPrefix = true },
                            new Service { Url = "https://microservice-two/swagger/", Name = "Microservices Two", Version = "1.0", RemoveApiPrefix = true },
                        }
                    }
                }
            };

            yield return new object[]
            {
                new SwaggerAggregatorData
                {
                    Version = "1.0",
                    ExpectedResultFile = "Samples/AggregatorDocumentWithBaseAuthentication.yaml",
                    HttpClientMockData = new Dictionary<string, string>
                    {
                        { "https://microservice-one/swagger/", "Samples/MockFirstDocument.yaml" },
                        { "https://microservice-two/swagger/", "Samples/MockSecondDocument.yaml" },
                    },
                    Options = new SwaggerAggregatorOptions
                    {
                        Info = new Info
                        {
                            Title = "Microservice Swagger Aggregator",
                            Description = "An example of Swagger Aggregator",
                            TermsOfService = "http://swagger.io/terms/",
                            Contact = new Contact
                            {
                                Email = "support@swagger.io"
                            },
                            License = new License
                            {
                                Name = "Apache 2.0",
                                Url = "http://www.apache.org/licenses/LICENSE-2.0.html"
                            }
                        },
                        Servers = new List<Server>
                        {
                            new Server { Url = "http://localhost", Description = "Localhost server URL" },
                        },
                        Services = new List<Service>
                        {
                            new Service { Url = "https://microservice-one/swagger/", Name = "Microservices One", Version = "1.0", RemoveApiPrefix = false },
                            new Service { Url = "https://microservice-two/swagger/", Name = "Microservices Two", Version = "1.0", RemoveApiPrefix = false },
                            new Service { Url = "https://microservice-one-v2/swagger", Name = "Microservices One", Version = "2.0", RemoveApiPrefix = false},
                        },
                        HttpAuthentication = new HttpAuthentication
                        {
                            Scheme = "basic"
                        }
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
