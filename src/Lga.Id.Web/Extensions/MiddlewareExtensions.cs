using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lga.Id.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSwaggerExtention(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app).UseSwaggerUI(options =>
            {                
                options.DocumentTitle = "LGA .id - API";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LGA .id API");


            });
            return app;
        }
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LGA.id Web API",
                    Version = "v1",
                    Description = "All front end UI should use this LGA.id web api",
                   
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });
            return services;
        }

       
    }
}
