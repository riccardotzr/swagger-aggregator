using System;
using Microsoft.OpenApi.Models;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISwaggerAggregatorService
    {

        #region DYNAMIC

        #region PROPERTIES
        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="version"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        Task<OpenApiDocument> GetAggregateSwagger(string title, string description, string version, List<Service> services);

        #endregion

        #endregion
    }
}

