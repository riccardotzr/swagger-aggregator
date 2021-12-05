using System;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class FileSystemService : IFileSystemService
    {
        #region DYNAMIC

        #region FIELDS
        #endregion

        #region CONSTRUCTORS
        #endregion

        #region PROPERTIES
        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<OpenApiDocument> ReadFile(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var stream = new StreamReader(fileStream, Encoding.UTF8);
            var content = await stream.ReadToEndAsync();
            var result = new OpenApiStringReader().Read(content, out var diagnostic);

            return result;
        }

        #endregion

        #endregion
    }
}