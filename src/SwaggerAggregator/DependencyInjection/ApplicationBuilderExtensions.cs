using System;
using Microsoft.Extensions.Options;
using SwaggerAggregator;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        #region DYNAMIC

        #region PUBLIC

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerAggregator(this IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseMiddleware<SwaggerAggregatorMiddleware>();

            return app;
        }
        #endregion

        #region PRIVATE
        #endregion

        #endregion
    }
}

