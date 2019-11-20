using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductsAPI.DbContexts;
using ProductsAPI.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Data.Common;
using RabbitMQ.Client;
using System.Reflection;
using MassTransit;
using MassTransit.Util;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace ProductsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMVC(Configuration)
               .AddCustomDbContext(Configuration)
               .AddIntegrationServices(Configuration);
              // .AddEventBus(Configuration);



            var container = new ContainerBuilder();
            container.Populate(services);
             return new AutofacServiceProvider(container.Build());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseMvc();
            
        }


    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductContext>(o => o.UseSqlServer(configuration.GetConnectionString("ProductDB")));
           

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<IProductRepository, ProductRepository>();
           
            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
