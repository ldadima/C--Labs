using System.Collections.Generic;
using System.ComponentModel;
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
        private MarketSystem _marketSystem;
        private Mapper _mapper;

        public BooksResponseConsumer(MarketSystem marketSystem, Mapper mapper)
        {
            _marketSystem = marketSystem;
            _mapper = mapper;
        }
        public Task Consume(ConsumeContext<IBookResponse> context)
        {
            var message = context.Message;
            var books = _mapper.Map<List<IBookResponse.Book>, List<Book>>(message.Books);
            _marketSystem.BookReception(books);
            return Task.CompletedTask;
        }
    }
}