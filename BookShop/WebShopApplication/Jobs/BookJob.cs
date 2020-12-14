using System;
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
        private readonly MarketSystem _marketSystem;

        public BookJob(MarketSystem marketSystem)
        {
            _marketSystem = marketSystem;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            Console.Out.WriteLine("Job заказ книг");
            if (_marketSystem.IsNeedSomeBooks())
            {
                await _marketSystem.DeliveryRequest(10);
            }
        }
    }
}