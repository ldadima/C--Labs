using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop;
using Shop.Infrastructure.EntityFramework;
using WebShopApplication.Producer;

namespace WebShopApplication.Services
{
    public class MarketSystem
    {
        private readonly ShopContextDbContextFactory _shopDbContextFactory;
        private readonly BookRequestProducer _producer;
        private ShopLibrary _shop;

        public MarketSystem(ShopContextDbContextFactory shopDbContextFactory, BookRequestProducer producer)
        {
            _shopDbContextFactory = shopDbContextFactory;
            _producer = producer;
            using var context = _shopDbContextFactory.GetContext();
            _shop = context.GetShopLibrary().Result;
        }

        public MarketSystem(ShopLibrary shopLibrary)
        {
            _shop = shopLibrary;
        }

        public List<Book> GetBooks()
        {
            if (_shop == null)
            {
                throw new InvalidOperationException("Shop not found in database");
            }
            return new List<Book>(_shop.Books);
        }

        public async Task<Book> SaleBook(long id)
        {
            await using var context = _shopDbContextFactory.GetContext();
            var shopLibrary = await context.GetShopLibrary();
            if (shopLibrary == null)
            {
                throw new InvalidOperationException("Shop not found in database");
            }
            var book = shopLibrary.SaleBook(id);
            await context.SaveChangesAsync();
            _shop = shopLibrary;
            Console.WriteLine($"Книга с id {id} продана");
            return book;
        }

        public bool IsNeedSomeBooks()
        {
            return _shop.IsFewBooksLeft() || _shop.HasManyOldBooks();
        }

        public async Task BookReception(IEnumerable<Book> books)
        {
            await using var context = _shopDbContextFactory.GetContext();
            var shopLibrary = await context.GetShopLibrary();
            var count = 0;
            try
            {
                count += books.Count(book => shopLibrary.TryAddBook(book));
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.Message);
            }
            
            if (_shopDbContextFactory != null)
            {
                await context.SaveChangesAsync();
            }

            _shop = shopLibrary;
            Console.WriteLine($"{count} книг принято");
        }

        public async Task DeliveryRequest(int count)
        {
            await _producer.SentBookRequest(count);
            Console.WriteLine("Заказ поставки системой");
        }

        public void BeginSale()
        {
            foreach (var book in _shop.Books)
            {
                switch (book.Genre)
                {
                    case "adventure":
                        book.ChangePrice(ShopLibrary.AdventureSale);
                        break;
                    case "fiction":
                        book.ChangePrice(ShopLibrary.FictionSale);
                        break;
                    case "encyclopedia":
                        book.ChangePrice(ShopLibrary.EncyclopediaSale);
                        break;
                }
            }

            Console.Out.WriteLine("Старт акции");
        }

        public void EndSale()
        {
            foreach (var book in _shop.Books)
            {
                book.ReturnPrice();
            }

            Console.Out.WriteLine("Конец акции");
        }
        
    }
}