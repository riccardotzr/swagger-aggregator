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
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<SwaggerAggregatorOptions>(Configuration);
            services.AddSwaggerAggregator();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseSwaggerAggregator(setup =>
            {
                var options = Configuration.Get<SwaggerAggregatorOptions>();

                setup.RoutePrefix = "documentation";
                setup.FileName = "swagger";
                setup.FileExtension = FileExtension.Yaml;
                setup.Versions = options.Services.GroupBy(x => x.Version).Select(x => x.Key).ToList();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
