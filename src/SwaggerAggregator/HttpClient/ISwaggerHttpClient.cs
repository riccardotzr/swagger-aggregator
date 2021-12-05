using System;
using Microsoft.OpenApi.Models;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISwaggerHttpClient
    {
        #region DYNAMIC

        #region PROPERTIES
        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>    
        Task<OpenApiDocument> GetOpenApiDocument(string endpoint);

        #endregion

        #endregion
    }
}

