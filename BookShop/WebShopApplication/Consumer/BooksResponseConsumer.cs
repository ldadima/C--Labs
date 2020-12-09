using System;
using System.Threading.Tasks;
#warning можно, кстати, не делать nuget пакет, а просто добавить референс на тот проект, где контрак лежит
#warning а то вот у меня сейчас не билдится
using ContractRabbit;
using MassTransit;

namespace WebShopApplication.Consumer
{
    public class BooksResponseConsumer : IConsumer<IBookResponse>
    {
        public Task Consume(ConsumeContext<IBookResponse> context)
        {
            #warning а почему только вывод в консоль и всё? эти книги должны сохраняться в базу 
            var message = context.Message;
            Console.WriteLine($"Books: {message.Books}");
            return Task.CompletedTask;
        }
    }
}