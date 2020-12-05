using System;
using System.Net.Http;
using BookDeliveryApplication.Consumer;
using BookDeliveryApplication.Producer;
using BookDeliveryApplication.Services;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookDeliveryApplication
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
            services.AddSingleton<IServiceProxy, ServiceBookProxy>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<BooksReceiveProducer>();
            services.AddControllers();
            services.AddMassTransit(isp =>
                {
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
                            "book-shop-queue", ep =>
                            {
                                ep.PrefetchCount = 1;
                                ep.ConfigureConsumer<BookRequestConsumer>(isp);
                            });
                    });
                },
                ispc =>
                {
                    ispc.AddConsumers(typeof(BookRequestConsumer).Assembly);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        
    }
}