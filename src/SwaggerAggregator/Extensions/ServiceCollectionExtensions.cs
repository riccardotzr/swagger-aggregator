using SwaggerAggregator;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerAggregator(this IServiceCollection services, Action<SwaggerAggregatorOptions> setupAction = null)
        {
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.AddHttpClient<ISwaggerHttpClient, SwaggerHttpClient>();

            services.AddTransient<ISwaggerProvider, SwaggerAggregatorProvider>();

            return services;
        }
    }
}
