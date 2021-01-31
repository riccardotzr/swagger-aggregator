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
    public class OpenApiDocumentBuilder
    {
        private readonly OpenApiDocument _document;

        /// <summary>
        /// 
        /// </summary>
        public OpenApiDocumentBuilder()
        {
            _document = new OpenApiDocument
            {
                Servers = new List<OpenApiServer>(),
                Paths = new OpenApiPaths(),
                Components = new OpenApiComponents()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetInfo(Info info, string version)
        {
            _document.Info = new OpenApiInfo
            {
                Title = info.Title,
                Description = info.Description,
                Version = version ?? "1.0"
            };

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetServer(List<Server> servers)
        {
            foreach (var item in servers)
            {
                _document.Servers.Add(new OpenApiServer
                {
                    Url = item.Url,
                    Description = item.Description
                });
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="removePrefix"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetPath(OpenApiPaths paths, bool removePrefix = false)
        {
            foreach (var item in paths)
            {
                if (removePrefix)
                {
                    var path = item.Key.Replace("/api", "");
                    _document.Paths[path] = item.Value;
                }
                else
                {
                    _document.Paths[item.Key] = item.Value;
                }
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemas"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetSchemas(IDictionary<string, OpenApiSchema> schemas)
        {
            foreach (var item in schemas)
            {
                if (!_document.Components.Schemas.ContainsKey(item.Key))
                {
                    _document.Components.Schemas.Add(item.Key, item.Value);
                }
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OpenApiDocument Build()
        {
            return _document;
        }
    }
}
