using SwaggerAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerAggregator(this IApplicationBuilder app, SwaggerAggregatorUIOptions options)
        {
            var documentName = "{documentName}";

            app.UseSwagger(setup =>
            {
                setup.RouteTemplate = $"{options.RoutePrefix}/{documentName}/{options.FileName}.{options.FileExtension}";
            });

            app.UseSwaggerUI(setup =>
            {
                foreach (var version in options.Versions)
                {
                    setup.SwaggerEndpoint($"/{options.RoutePrefix}/{version}/{options.FileName}.{options.FileExtension}", version);
                    setup.RoutePrefix = options.RoutePrefix;
                }
            });

            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerAggregator(this IApplicationBuilder app, Action<SwaggerAggregatorUIOptions> setupAction)
        {
            var options = new SwaggerAggregatorUIOptions();
            setupAction(options);

            return app.UseSwaggerAggregator(options);
        }
    }
}
