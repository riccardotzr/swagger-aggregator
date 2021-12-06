using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SwaggerAggregator.UnitTests
{
    public class MockHttpMessageHandler : DelegatingHandler
    {
        private readonly HttpResponseMessage _response;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response ?? throw new ArgumentException(nameof(response));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_response);
        }
    }
}
