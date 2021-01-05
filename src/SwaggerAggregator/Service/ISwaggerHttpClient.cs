using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISwaggerHttpClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        OpenApiDocument GetOpenApiDocument(string endpoint);
    }
}
