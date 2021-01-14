using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop;
using ContractRabbit;
using MassTransit;
using WebShopApplication.Services;

namespace WebShopApplication.Consumer
{
    public class BooksResponseConsumer : IConsumer<IBookResponse>
    {
        private readonly MarketSystem _marketSystem;

        public BooksResponseConsumer(MarketSystem marketSystem)
        {
            _marketSystem = marketSystem;
        }
        public async Task Consume(ConsumeContext<IBookResponse> context)
        {
            Console.Out.WriteLine("Сообщение с книгами принято");
            var message = context.Message;
            var books = new List<Book>();
            foreach (var book in message.Books)
            {
                books.Add(new Book(0, book.Title, book.Genre, book.IsNew, book.Price, book.DateOfDelivery));
            }
            await _marketSystem.BookReception(books);
        }
    }
}