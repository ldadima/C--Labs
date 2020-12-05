using System;
using System.Threading.Tasks;
using ContractRabbit;
using MassTransit;

namespace BookDeliveryApplication.Consumer
{
    public class BookRequestConsumer: IConsumer<IBookRequest>
    {
        public Task Consume(ConsumeContext<IBookRequest> context)
        {
            var message = context.Message;
            Console.WriteLine($"Count of book {message.BookCount}");
            return Task.CompletedTask;
        }
    }
}