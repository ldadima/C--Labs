using System;
using System.Net.Http;
using ApplicationServises;
using BookShop;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Infrastructure.EntityFramework;
using WebShopApplication.Bootstrap;
using WebShopApplication.Consumer;
using WebShopApplication.Producer;
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
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSingleton(provider =>
                new BooksContextDbContextFactory(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<HttpClient>();
            services.AddSingleton<MarketSystem>();
            services.AddSingleton<IServiceProxy, ServiceProxy>();
            services.AddControllers();
            services.AddBackgroundJobs();
            services.AddSingleton<BookRequestProducer>();
            services.AddMassTransit(isp =>
                {
#warning на этой строке полетит exception, потому что у тебя в конфиге нет секции MassTransit
#warning значит и данные для подключения к RMQ брать приложению неоткуда
#warning это ещё раз наталкивает на мысль, что ты не запускал и не проверял приложение
                    var hostConfig = new MassTransitConfiguration();
                    Configuration.GetSection("MassTransit").Bind(hostConfig);

                    return Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(
                            new Uri(hostConfig.RabbitMqAddress),
                            h =>
                            {
                                h.Username(hostConfig.UserName);
                                h.Password(hostConfig.Password);
                            });

                        cfg.Durable = hostConfig.Durable;
                        cfg.PurgeOnStartup = hostConfig.PurgeOnStartup;

                        cfg.ReceiveEndpoint(host,
                            "books-delivery-queue", ep =>
                            {
                                ep.PrefetchCount = 1;
                                ep.ConfigureConsumer<BooksResponseConsumer>(isp);
                            });
                    });
                },
                ispc => { ispc.AddConsumers(typeof(BooksResponseConsumer).Assembly); });
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