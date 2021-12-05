using System;
using Microsoft.OpenApi.Models;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileSystemService
    {
        #region DYNAMIC

        #region PROPERTIES
        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<OpenApiDocument> ReadFile(string path);

        #endregion

        #endregion
    }
}

