using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Lga.Id.Core.Interfaces;
using Lga.Id.Core.Interfaces.Repositories;
using Lga.Id.Core.Interfaces.Services;
using Lga.Id.Core.Services;
using Lga.Id.Infrastructure.Data;
using Lga.Id.Infrastructure.Logging;
using Lga.Id.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Lga.Id.Web.Extensions;
using AutoMapper;
using Microsoft.Extensions.Hosting; 


namespace Lga.Id.Web
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddRazorPages(); 

            services.AddSwaggerExtension();

            //services.AddApiVersioning();
            services.ConfigureCors();
            services.AddBusinessExceptionFilter();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //services.AddAutoMapper(typeof(Startup));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();            
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));           
            services.AddScoped<IScoreService, ScoreService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           

            _services = services;

        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //services.AddDbContext<RefactorThisDatabaseContext>(conn =>
            //    conn.UseSqlServer(Configuration.GetConnectionString("WebApiConnectionString")));

            services.AddDbContext<LgaIdDatabaseContext>(conn =>
                conn.UseSqlite(Configuration.GetConnectionString("WebApiSQLiteConnectionString")));

            ConfigureServices(services);

        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            //services.AddDbContext<RefactorThisDatabaseContext>(conn => 
            //    conn.UseSqlServer(Configuration.GetConnectionString("WebApiConnectionString")));

            services.AddDbContext<LgaIdDatabaseContext>(conn =>
              conn.UseSqlite(Configuration.GetConnectionString("WebApiSQLiteConnectionString")));

            ConfigureServices(services);

           
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseHsts();
            }

            app.UseSwaggerExtention(); 
            app.UseHttpsRedirection();
            app.UseStaticFiles(); 

            //app.UseEndpoints(endpoint => 
            //{
            //    endpoint.MapRazorPages(); 

            //});

            
        }
    }
}
