using System;
using System.Threading.Tasks;
using ContractRabbit;
using MassTransit;

namespace WebShopApplication.Consumer
{
    public class BooksResponseConsumer : IConsumer<IBookResponse>
    {
        public Task Consume(ConsumeContext<IBookResponse> context)
        {
            var message = context.Message;
            Console.WriteLine($"Books: {message.Books}");
            return Task.CompletedTask;
        }
    }
}