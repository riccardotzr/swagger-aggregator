using System;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace SwaggerAggregator
{

    /// <summary>
    /// 
    /// </summary>
    public class SwaggerHttpClient : ISwaggerHttpClient
    {
        #region DYNAMIC

        #region FIELDS

        private readonly HttpClient _client;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public SwaggerHttpClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        #endregion

        #region PROPERTIES
        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<OpenApiDocument> GetOpenApiDocument(string endpoint)
        {
            try
            {
                var stream = await _client.GetStringAsync(endpoint);
                var document = new OpenApiStringReader().Read(stream, out var diagnostic);

                // if (diagnostic != null && diagnostic.Errors.Any())
                // {
                //     throw new ReaderException($"An error has occurred while reading content from: {endpoint}", endpoint, diagnostic.Errors);
                // }

                return document;
            }
            catch (Exception ex)
            {
                //     if (ex is ReaderException)
                //     {
                //         throw ex as ReaderException;
                //     }

                throw new Exception($"The operation could not be completed. Endpoint: {endpoint}", ex);
            }
        }

        #endregion

        #endregion
    }
}

