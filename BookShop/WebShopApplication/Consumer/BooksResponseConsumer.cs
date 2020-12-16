using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookShop;
using ContractRabbit;
using MassTransit;
using WebShopApplication.Services;

namespace WebShopApplication.Consumer
{
    public class BooksResponseConsumer : IConsumer<IBookResponse>
    {
        private readonly MarketSystem _marketSystem;
        private readonly Mapper _mapper;

        public BooksResponseConsumer(MarketSystem marketSystem, Mapper mapper)
        {
            _marketSystem = marketSystem;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<IBookResponse> context)
        {
            Console.Out.WriteLine("Сообщение с книгами принято");
            var message = context.Message;
            var books = _mapper.Map<List<IBookResponse.Book>, List<Book>>(message.Books);
            await _marketSystem.BookReception(books);
        }
    }
}