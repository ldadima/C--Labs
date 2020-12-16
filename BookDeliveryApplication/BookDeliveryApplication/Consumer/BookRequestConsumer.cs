using System;
using System.Threading.Tasks;
using BookDeliveryApplication.Services;
using ContractRabbit;
using MassTransit;

namespace BookDeliveryApplication.Consumer
{
    public class BookRequestConsumer: IConsumer<IBookRequest>
    {
        private readonly IServiceProxy _serviceProxy;

        public BookRequestConsumer(IServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
        }

        public Task Consume(ConsumeContext<IBookRequest> context)
        {
            var message = context.Message;
            Console.WriteLine($"Count of book {message.BookCount}");
            _serviceProxy.GetAndSaveBooks(message.BookCount);
            return Task.CompletedTask;
        }
    }
}