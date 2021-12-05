using System;
using SwaggerAggregator;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        #region DYNAMIC

        #region PUBLIC

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerAggregator(this IServiceCollection services, IConfiguration configuration)
        {
            // Controllers
            services.AddControllers();
            
            // Options
            services.AddSwaggerAggregatorOptions(configuration);

            // HttpClient
            services.AddSwaggerAggregatorHttpClient();

            // Services
            services.AddSwaggerAggregatorServices();

            return services;
        }

        #endregion

        #region PRIVATE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AddSwaggerAggregatorOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerAggregatorOptions>(configuration);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddSwaggerAggregatorHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<ISwaggerHttpClient, SwaggerHttpClient>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddSwaggerAggregatorServices(this IServiceCollection services)
        {
            services.AddTransient<ISwaggerAggregatorService, SwaggerAggregatorService>();
            services.AddTransient<IFileSystemService, FileSystemService>();

            return services;
        }

        #endregion

        #endregion
    }
}

