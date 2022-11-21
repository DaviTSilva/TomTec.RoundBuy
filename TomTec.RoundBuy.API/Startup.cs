using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;

namespace TomTec.RoundBuy.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<RoundBuyDbContext>();
            services.AddControllers();
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));

            //Exceptions Handlings
            services.AddScoped<KeyNotFoundExceptionFilterAttribute>();
            services.AddScoped<UnauthorizedAccessExceptionFilterAttribute>();
            services.AddScoped<GenericExceptionFilterAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile($"D:\\TomTec\\RoundBuy\\Logs\\log-roundbuy-api_{DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss")}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(op => op
                .WithOrigins(new[]{@"http://localhost:3000",
                    @"http://localhost:8080",
                    @"http://localhost:4200",
                    @"https://localhost:44392",
                    @"https://roundbuy.vercel.app" })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
