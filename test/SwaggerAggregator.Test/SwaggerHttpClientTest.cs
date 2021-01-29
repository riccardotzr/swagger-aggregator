using FluentAssertions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
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
        [Fact(DisplayName = "")]
        public async Task Should_Returns_HttpStatus_Code_200()
        { 
            var content = Resources.GetFileContent("Samples/swagger.yaml");

            var client = new HttpClient(new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content),
            }));

            var swaggerClient = new SwaggerHttpClient(client);
            var actualResult = await swaggerClient.GetOpenApiDocument("https://my-microservice.com/");

            var expectedResult = new OpenApiDocument()
            {
                Info = new OpenApiInfo
                {
                    Title = "Microservices Swagger Aggregator",
                    Description = "Swagger Aggregator Microservices",
                    TermsOfService = new Uri("http://swagger.io/terms/"),
                    Contact = new OpenApiContact
                    {
                        Email = "email: apiteam@swagger.io"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                    },
                    Version = "1.0.0"
                },
                Servers = new List<OpenApiServer>
                {
                    new OpenApiServer { Url = "https://microservices_one/v2", Description = "Swagger Microservice One" },
                    new OpenApiServer { Url = "https://microservices_two/v2", Description = "Swagger Microservice Two" }
                },
                Tags = new List<OpenApiTag>
                {
                    new OpenApiTag
                    {
                        Name = "pet",
                        Description = "Everything about your Pets",
                        ExternalDocs = new OpenApiExternalDocs
                        {
                            Url = new Uri("http://swagger.io"),
                            Description = "Find out more"
                        }
                    }
                },
                Paths = new OpenApiPaths { },
            };

            actualResult.Should().NotBeNull();
            //actualResult.Should().BeEquivalentTo(expectedResult);
        }

        
    }
}
