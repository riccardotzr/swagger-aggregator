using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(builder => 
                {
                    var path = Environment.GetEnvironmentVariable("SWAGGER_AGGREGATOR_CONFIG");
                    
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile(path: path, optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();
                });
    }
}
