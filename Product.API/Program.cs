using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ProductsAPI.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
//using ECom.WebHost.Customization;

namespace ProductsAPI
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static int Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var configuration = GetConfiguration();
            var host = BuildWebHost(configuration, args);
            //Log.Information("Applying migrations ({ApplicationContext})...", AppName);

            //host.MigrateDbContext<ProductContext>((context, services) =>
            //{
            //    var env = services.GetService<IHostingEnvironment>();
                // var settings = services.GetService<IOptions<CatalogSettings>>();
                // var logger = services.GetService<ILogger<CatalogContextSeed>>();

                // new CatalogContextSeed()
                //     .SeedAsync(context, env, settings, logger)
                //     .Wait();
            //});
           // .MigrateDbContext<IntegrationEventLogContext>((_, __) => { });

           // Log.Information("Starting web host ({ApplicationContext})...", AppName);
            host.Run();

            return 0;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .CaptureStartupErrors(false)
               .UseStartup<Startup>()
               .UseApplicationInsights()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseWebRoot("Pics")
               .UseConfiguration(configuration)
               //.UseSerilog()
               .Build();

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }
    }
}
