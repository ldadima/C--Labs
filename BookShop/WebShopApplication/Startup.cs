﻿using System.Net.Http;
using ApplicationServises;
using BookShop;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Infrastructure.EntityFramework;
using WebShopApplication.Bootstrap;
using WebShopApplication.Services;

namespace WebShopApplication
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
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSingleton(provider =>
                new BooksContextDbContextFactory(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<HttpClient>();
            services.AddSingleton<MarketSystem>();
            services.AddSingleton<IServiceProxy, ServiceProxy>();
            services.AddControllers();
            services.AddBackgroundJobs();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseMvc();
        }
    }
}