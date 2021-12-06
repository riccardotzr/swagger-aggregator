using System;
using Microsoft.OpenApi.Models;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenApiDocumentBuilder
    {
        #region DYNAMIC

        #region FIELDS

        private readonly OpenApiDocument _document;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// 
        /// </summary>
        public OpenApiDocumentBuilder()
        {
            _document = new OpenApiDocument
            {
                Info = new OpenApiInfo(),
                Servers = new List<OpenApiServer>(),
                Tags = new List<OpenApiTag>(),
                Paths = new OpenApiPaths(),
                Components = new OpenApiComponents()
            };
        }

        #endregion

        #region PROPERTIES
        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetInfo(string title, string description, string version)
        {
            _document.Info = new OpenApiInfo
            {
                Title = title,
                Description = description,
                Version = version
            };

            return this;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>

        public OpenApiDocumentBuilder SetServers(IList<OpenApiServer> servers)
        {
            //var serversToAdd = _document.Servers.Where(x => servers.Any(y => y.Url == x.Url)).ToList();
            var serversToAdd = servers.Where(x => !_document.Servers.Any(y => y.Url == x.Url)).ToList();

            if (serversToAdd != null && serversToAdd.Any())
            {
                foreach (var item in serversToAdd)
                {
                    _document.Servers.Add(item);
                }
            }

            // foreach (var server in servers)
            // {
            //     if (!_document.Servers.Contains(server))
            //     {
            //         _document.Servers.Add(server);
            //     }
            // }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetTags(IList<OpenApiTag> tags)
        {
            foreach (var tag in tags)
            {
                if (!_document.Tags.Contains(tag))
                {
                    _document.Tags.Add(tag);
                }
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="removePrefix"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetPaths(OpenApiPaths paths)
        {
            foreach (var item in paths)
            {
                if (!_document.Paths.Contains(item))
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
        /// <param name="securitySchemes"></param>
        /// <returns></returns>
        public OpenApiDocumentBuilder SetSecurityScehems(IDictionary<string, OpenApiSecurityScheme> securitySchemes)
        {
            foreach (var item in securitySchemes)
            {
                if (!_document.Components.SecuritySchemes.ContainsKey(item.Key))
                {
                    _document.Components.SecuritySchemes.Add(item);
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

        #endregion

        #endregion
    }
}

