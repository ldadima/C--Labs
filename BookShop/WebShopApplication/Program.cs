﻿using System;
using System.Collections.Generic;
using AutoMapper;
using BookShop;
using ContractRabbit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebShopApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}