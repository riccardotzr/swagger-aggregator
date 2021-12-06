using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SwaggerAggregator.UnitTests
{
    public class MockHttpMessageHandlerSwaggerHttpClient : DelegatingHandler
    {
        private readonly Dictionary<string, string> _ExpectedResults;

        public MockHttpMessageHandlerSwaggerHttpClient(Dictionary<string, string> expectedResults)
        {
            _ExpectedResults = expectedResults ?? throw new ArgumentNullException(nameof(expectedResults));
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_ExpectedResults.ContainsKey(request.RequestUri?.AbsoluteUri))
            {
                var contentString = _ExpectedResults.FirstOrDefault(x => x.Key == request.RequestUri.AbsoluteUri).Value;
                var content = Resources.GetFileContent(contentString);

                return await Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                });
            }
            else
            {
                return await Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });
            }
            
        }
    }
}
