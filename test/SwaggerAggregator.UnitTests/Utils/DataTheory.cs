using System;
using System.Collections;
using System.Collections.Generic;

namespace SwaggerAggregator.UnitTests
{
    public class SwaggerAggregatorTestData
    {
        public string ExpectedAggregatedResultFile { get; set; }

        public string HttpClientMockFile { get; set; }

        public string FileSystemMockFile { get; set; }

        public SwaggerAggregatorOptions Options { get; set; }
    }

    public class SwaggerAggregatorOptionsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Test HTTP aggregation only
            yield return new object[]
            {
                new SwaggerAggregatorTestData
                {
                    ExpectedAggregatedResultFile = "Utils/Samples/SwaggerAggregator/MockFirstDocument.yaml",
                    HttpClientMockFile = "Utils/Samples/SwaggerAggregator/MockFirstDocument.yaml",
                    FileSystemMockFile = "Utils/Samples/SwaggerAggregator/MockSecondDocument.yaml",
                    Options = new SwaggerAggregatorOptions
                    {
                        Version = "1.0.0",
                        Description = "Test Http Aggregator",
                        Title = "My Swagger Aggregator",
                        Services = new List<Service> 
                        {
                            new Service
                            {
                                Type = "url",
                                Url = "https://microservice-one/swagger/",
                            }
                        },
                        Route = "/documentation/swagger.{json|yaml}"
                    }
                }
            };

            // Test File aggregation only
            yield return new object[]
            {
                new SwaggerAggregatorTestData
                {
                    ExpectedAggregatedResultFile = "Utils/Samples/SwaggerAggregator/MockSecondDocument.yaml",
                    HttpClientMockFile = "Utils/Samples/SwaggerAggregator/MockFirstDocument.yaml",
                    FileSystemMockFile = "Utils/Samples/SwaggerAggregator/MockSecondDocument.yaml",
                    Options = new SwaggerAggregatorOptions
                    {
                        Version = "1.0.0",
                        Description = "Test Http Aggregator",
                        Title = "My Swagger Aggregator",
                        Services = new List<Service> 
                        {
                            new Service
                            {
                                Type = "file",
                                Url = "/app/swagger.yaml",
                            }
                        },
                        Route = "/documentation/swagger.{json|yaml}"
                    }
                }
            };

            // Test HTTP + File aggregation
            yield return new object[]
            {
                new SwaggerAggregatorTestData
                {
                    ExpectedAggregatedResultFile = "Utils/Samples/SwaggerAggregator/MockAggregateDocument.yaml",
                    HttpClientMockFile = "Utils/Samples/SwaggerAggregator/MockFirstDocument.yaml",
                    FileSystemMockFile = "Utils/Samples/SwaggerAggregator/MockSecondDocument.yaml",
                    Options = new SwaggerAggregatorOptions
                    {
                        Version = "1.0.0",
                        Description = "Test Http + File Aggregator",
                        Title = "My Swagger Aggregator",
                        Services = new List<Service> 
                        {
                            new Service
                            {
                                Type = "url",
                                Url = "https://microservice-one/swagger/",
                            },
                            new Service
                            {
                                Type = "file",
                                Path = "app/swagger.yaml"
                            }
                        },
                        Route = "/documentation/swagger.{json|yaml}"
                    }
                }
            };

            // Test HTTP + File Aggregation + Include Paths
            yield return new object[]
            {
                new SwaggerAggregatorTestData
                {
                    ExpectedAggregatedResultFile = "Utils/Samples/SwaggerAggregator/MockAggregateDocumentInclude.yaml",
                    HttpClientMockFile = "Utils/Samples/SwaggerAggregator/MockFirstDocument.yaml",
                    FileSystemMockFile = "Utils/Samples/SwaggerAggregator/MockSecondDocument.yaml",
                    Options = new SwaggerAggregatorOptions
                    {
                        Version = "1.0.0",
                        Description = "Test Http + File Aggregator",
                        Title = "My Swagger Aggregator",
                        Services = new List<Service> 
                        {
                            new Service
                            {
                                Type = "url",
                                Url = "https://microservice-one/swagger/",
                                IncludePaths = new List<ApiPath> 
                                {
                                    new ApiPath { Path = "/pet" },
                                    new ApiPath { Path = "/pet/{petId}" }
                                }
                            },
                            new Service
                            {
                                Type = "file",
                                Path = "app/swagger.yaml"
                            }
                        },
                        Route = "/documentation/swagger.{json|yaml}"
                    }
                }
            };

             // Test HTTP + File Aggregation + Exclude Paths
            yield return new object[]
            {
                new SwaggerAggregatorTestData
                {
                    ExpectedAggregatedResultFile = "Utils/Samples/SwaggerAggregator/MockAggregateDocumentExclude.yaml",
                    HttpClientMockFile = "Utils/Samples/SwaggerAggregator/MockFirstDocument.yaml",
                    FileSystemMockFile = "Utils/Samples/SwaggerAggregator/MockSecondDocument.yaml",
                    Options = new SwaggerAggregatorOptions
                    {
                        Version = "1.0.0",
                        Description = "Test Http + File Aggregator",
                        Title = "My Swagger Aggregator",
                        Services = new List<Service> 
                        {
                            new Service
                            {
                                Type = "url",
                                Url = "https://microservice-one/swagger/",
                                EscludePaths = new List<ApiPath> 
                                {
                                    new ApiPath { Path = "/pet" },
                                    new ApiPath { Path = "/pet/{petId}" }
                                }
                            },
                            new Service
                            {
                                Type = "file",
                                Path = "app/swagger.yaml"
                            }
                        },
                        Route = "/documentation/swagger.{json|yaml}"
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
