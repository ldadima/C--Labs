﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Quartz;
using WebShopApplication.Services;

namespace WebShopApplication.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class BookJob  : IJob
    {
        private readonly IServiceProxy _serviceProxy;

        public BookJob(IServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
        }
        public Task Execute(IJobExecutionContext context)
        {
            Console.Out.WriteLine("Test");
             _serviceProxy.GetAndSaveBooks();
            return Task.CompletedTask;
        }
    }
}