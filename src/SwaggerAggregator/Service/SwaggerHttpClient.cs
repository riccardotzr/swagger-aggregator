using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerHttpClient : ISwaggerHttpClient
    {
        private readonly HttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public SwaggerHttpClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc />
        public async Task<OpenApiDocument> GetOpenApiDocument(string endpoint)
        {
            var stream = await _client.GetStreamAsync(endpoint);
            var document = new OpenApiStreamReader().Read(stream, out var diagnostic);

            if (diagnostic != null && diagnostic.Errors.Any())
            {
                throw new Exception("");
            }

            return document;
        }
    }
}
