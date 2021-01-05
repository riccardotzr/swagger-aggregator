using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerAggregatorProvider : ISwaggerProvider
    {
        private readonly SwaggerAggregatorOptions _options;
        private readonly ISwaggerHttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="client"></param>
        public SwaggerAggregatorProvider(IOptions<SwaggerAggregatorOptions> options, ISwaggerHttpClient client)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc />
        public OpenApiDocument GetSwagger(string documentName, string host = null, string basePath = null)
        {
            throw new NotImplementedException();
        }
    }
}
