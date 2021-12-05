
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    /// <summary>
    /// 
    /// </summary>
    public class  Startup
    {
        #region DYNAMIC

        #region FIELDS
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IConfiguration Configuration { get; set;}

        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddSwaggerAggregator(Configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerAggregator();
        }

        #endregion

        #endregion
    }
}