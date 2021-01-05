using Microsoft.OpenApi.Models;
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
        public OpenApiDocument GetOpenApiDocument(string endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
