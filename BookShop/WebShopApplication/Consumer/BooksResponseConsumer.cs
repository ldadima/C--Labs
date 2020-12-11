using System.ComponentModel;
using System.Threading.Tasks;
using ApplicationServices;
using ContractRabbit;
using MassTransit;

namespace WebShopApplication.Consumer
{
    public class BooksResponseConsumer : IConsumer<IBookResponse>
    {
        private MarketSystem _marketSystem;

        public BooksResponseConsumer(MarketSystem marketSystem)
        {
            _marketSystem = marketSystem;
        }
        public Task Consume(ConsumeContext<IBookResponse> context)
        {
            #warning а почему только вывод в консоль и всё? эти книги должны сохраняться в базу 
            var message = context.Message;
            return Task.CompletedTask;
        }
    }
}