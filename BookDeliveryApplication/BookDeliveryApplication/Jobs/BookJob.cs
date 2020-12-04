using System;
using System.Threading.Tasks;
using BookDeliveryApplication.Services;
using JetBrains.Annotations;
using Quartz;

namespace BookDeliveryApplication.Jobs
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