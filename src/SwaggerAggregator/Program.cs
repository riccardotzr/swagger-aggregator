using Microsoft.AspNetCore.Hosting;

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
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var configFile = Environment.GetEnvironmentVariable("SWAGGER_AGGREGATOR_CONFIG");

                    builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(path: configFile, optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();
                });
    
    }
}