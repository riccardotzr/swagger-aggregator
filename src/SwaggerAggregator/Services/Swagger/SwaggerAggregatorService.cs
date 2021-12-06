using System;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerAggregatorService : ISwaggerAggregatorService
    {
        #region DYNAMIC

        #region FIELDS

        private readonly ISwaggerHttpClient _httpClient;
        private readonly IFileSystemService _fileSistemService;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="fileSystemService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SwaggerAggregatorService(ISwaggerHttpClient httpClient, IFileSystemService fileSystemService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _fileSistemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }

        #endregion

        #region PROPERTIES
        #endregion

        #region METHODS

        #region PUBLIC

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<OpenApiDocument> GetAggregateSwagger(string title, string description, string version, List<Service> services)
        {
            var fileDocumentations = services.Where(x => x.Type == "file").ToList();
            var urlDocumentations = services.Where(x => x.Type == "url").ToList();

            var builder = new OpenApiDocumentBuilder()
                .SetInfo(title, description, version);
             
            if (fileDocumentations != null && fileDocumentations.Any())
            {
                await BuildOpenApiDocumentFromFiles(fileDocumentations, builder);
            }

            if (urlDocumentations != null && urlDocumentations.Any())
            {
                await BuildOpenApiDocumentFromUrls(urlDocumentations, builder);
            }

            return builder.Build();
        }

        #endregion

        #region PRIVATE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task BuildOpenApiDocumentFromUrls(List<Service> services, OpenApiDocumentBuilder builder)
        {
            foreach (var service in services)
            {
                var currentServiceDocument = await _httpClient.GetOpenApiDocument(service.Url);

                if (currentServiceDocument != null)
                {
                    if (service.IncludePaths != null && service.IncludePaths.Any())
                    {
                        BuildIncludePaths(currentServiceDocument, service.IncludePaths);
                    }

                    if (service.EscludePaths != null && service.EscludePaths.Any())
                    {
                        BuildEscludePaths(currentServiceDocument, service.EscludePaths);
                    }

                    BuildDocument(currentServiceDocument, builder);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        private async Task BuildOpenApiDocumentFromFiles(List<Service> services, OpenApiDocumentBuilder builder)
        {
            foreach (var service in services)
            {
                var currentServiceDocument = await _fileSistemService.ReadFile(service.Path);

                if (currentServiceDocument != null)
                {
                    if (service.IncludePaths != null && service.IncludePaths.Any())
                    {
                        BuildIncludePaths(currentServiceDocument, service.IncludePaths);
                    }

                    if (service.EscludePaths != null && service.EscludePaths.Any())
                    {
                        BuildEscludePaths(currentServiceDocument, service.EscludePaths);
                    }

                    BuildDocument(currentServiceDocument, builder);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentServiceDocument"></param>
        /// <param name="includePaths"></param>
        private void BuildIncludePaths(OpenApiDocument currentServiceDocument, List<ApiPath> includePaths) 
        {
            var currentServicePaths = currentServiceDocument.Paths;
            var currentServiceTags = currentServiceDocument.Tags;
            var pathsToInclude = includePaths.Select(x => x.Path).ToList();

            var openApiPathFiltered = currentServicePaths.Where(x => pathsToInclude.Contains(x.Key)).ToList();
            
            if (openApiPathFiltered != null && openApiPathFiltered.Any())
            {
                var paths = new OpenApiPaths();

                foreach (var item in openApiPathFiltered)
                {
                    paths[item.Key] = item.Value;
                }

                var tags = RemoveUnsedTags(currentServiceTags, paths);                

                currentServiceDocument.Paths = paths;
                currentServiceDocument.Tags = tags;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentServiceDocument"></param>
        /// <param name="escludePaths"></param>
        private void BuildEscludePaths(OpenApiDocument currentServiceDocument, List<ApiPath> escludePaths) 
        {
            var currentServicePaths = currentServiceDocument.Paths;
            var currentServiceTags = currentServiceDocument.Tags;
            var pathsToEsclude = escludePaths.Select(x => x.Path).ToList();

            var openApiPathFiltered = currentServicePaths.Where(x => !pathsToEsclude.Contains(x.Key)).ToList();
            
            if (openApiPathFiltered != null && openApiPathFiltered.Any())
            {
                var paths = new OpenApiPaths();

                foreach (var item in openApiPathFiltered)
                {
                    paths[item.Key] = item.Value;
                }

                var tags = RemoveUnsedTags(currentServiceTags, paths);                

                currentServiceDocument.Paths = paths;
                currentServiceDocument.Tags = tags;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentServiceTags"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        private IList<OpenApiTag> RemoveUnsedTags(IList<OpenApiTag> currentServiceTags, OpenApiPaths paths) 
        {
            var result = new List<OpenApiTag>();

            var pathTags = paths.Select(x => x.Value)
                .SelectMany(x => x.Operations)
                .SelectMany(x => x.Value.Tags)
                .ToList();

            if (pathTags != null && pathTags.Any())
            {
                result = pathTags.Where(x => currentServiceTags.Contains(x)).ToList();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentServiceDocument"></param>
        /// <param name="builder"></param>
        private void BuildDocument(OpenApiDocument currentServiceDocument, OpenApiDocumentBuilder builder) 
        {
            if (currentServiceDocument.Servers != null && currentServiceDocument.Servers.Any())
            {
                builder.SetServers(currentServiceDocument.Servers);
            }

            if (currentServiceDocument.Tags != null && currentServiceDocument.Tags.Any())
            {
                builder.SetTags(currentServiceDocument.Tags);
            }

            if (currentServiceDocument.Paths != null && currentServiceDocument.Paths.Any())
            {
                builder.SetPaths(currentServiceDocument.Paths);
            }

            if (currentServiceDocument.Components != null && currentServiceDocument.Components.Schemas.Any())
            {
                builder.SetSchemas(currentServiceDocument.Components.Schemas);
            }

            if (currentServiceDocument.Components?.SecuritySchemes != null && currentServiceDocument.Components.SecuritySchemes.Any())
            {
                builder.SetSecurityScehems(currentServiceDocument.Components.SecuritySchemes);
            }
        }

        #endregion
        
        #endregion

        #endregion
    }
}

