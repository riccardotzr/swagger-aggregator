using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SwaggerAggregator.Test
{
    public class MockHttpMessageHandlerSwaggerProvider : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var firstMicroservicesExpectedResultStream = Resources.GetFileContent("Samples/MockFirstDocument.yaml");
            var secondMicroserviceExpectedResultStream = Resources.GetFileContent("Samples/MockSecondDocument.yaml");

            if (request.RequestUri.AbsoluteUri.Contains("microservice-one"))
            {
                return await Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(firstMicroservicesExpectedResultStream)
                });
            }
            else
            {
                return await Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(secondMicroserviceExpectedResultStream)
                });
            }
        }
    }
}
