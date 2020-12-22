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
        private readonly ShopContextDbContextFactory _dbContextFactory;
        private readonly ShopLibrary _shopLibrary;
        private readonly BookRequestProducer _producer;

        public MarketSystem(ShopContextDbContextFactory dbContextFactory, BookRequestProducer producer)
        {
            _dbContextFactory = dbContextFactory;
            _producer = producer;
            using var dbContext = _dbContextFactory.GetContext();
            _shopLibrary = dbContext.GetShopLibrary().Result[0];
        }

        public List<Book> GetBooks()
        {
            return new List<Book>(_shopLibrary.Books);
        }

        public async Task SaleBook(long id)
        {
            using var context = _dbContextFactory.GetContext();
            var shopLibrary = await context.GetShopLibrary();
            #warning check shopLibrary != null
            shopLibrary.DeleteBook(id);
#warning заменить на метод
            shopLibrary.Balance = _shopLibrary.Balance;
            // context.UpdateShopLibrary(_shopLibrary);
            await context.SaveChangesAsync();
            Console.WriteLine($"Книга с id {id} продана");
        }

        public bool IsNeedSomeBooks()
        {
            return IsFewBooksLeft() || HasManyOldBooks();
        }

        public async Task BookReception(IEnumerable<Book> books)
        {
            var tmpList = new List<Book>();
                         foreach (var book in books)
                         {
                             if (_shopLibrary.Capacity == _shopLibrary.Books.Count)
                             {
                                 throw new OutOfMemoryException("Storage of library is full");
                             }
             
                             if (_shopLibrary.Books.FirstOrDefault(bookL => bookL.Id == book.Id) != null)
                             {
                                 Console.WriteLine($"Книга с id {book.Id} уже есть");
                                 continue;
                             }
             
                             if (!ReduceBalance(book.CurrentPrice * 0.07))
                             {
                                 Console.WriteLine($"На книгу c id {book.Id} по цене {book.CurrentPrice * 0.07} не хватает денег");
                                 continue;
                             }
             
                             _shopLibrary.Books.Add(book);
                             tmpList.Add(book);
                         }

            if (_dbContextFactory != null)
            {
                using var context = _dbContextFactory.GetContext();
                var shopLibrary = context.GetShopLibrary().Result[0];
                shopLibrary.Books.AddRange(tmpList);
                shopLibrary.Balance = _shopLibrary.Balance;
                // context.UpdateShopLibrary(_shopLibrary);
                await context.SaveChangesAsync();
            }

            Console.WriteLine($"{tmpList.Count} книг принято");
        }

        public async Task DeliveryRequest(int count)
        {
            await _producer.SentBookRequest(count);
            Console.WriteLine("Заказ поставки системой");
        }

        public void BeginSale()
        {
            foreach (var book in _shopLibrary.Books)
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
            foreach (var book in _shopLibrary.Books)
            {
                book.ReturnPrice();
            }

            Console.Out.WriteLine("Конец акции");
        }


        private void AddBalance(double plus)
        {
            _shopLibrary.Balance += plus;
        }

        private bool ReduceBalance(double reduce)
        {
            if (reduce > _shopLibrary.Balance)
            {
                return false;
            }

            _shopLibrary.Balance -= reduce;
            return true;
        }

        private bool IsFewBooksLeft()
        {
            return _shopLibrary.Books.Count / (double) _shopLibrary.Capacity <= ShopLibrary.PercentLeft;
        }

        private bool HasManyOldBooks()
        {
            double count = _shopLibrary.Books.FindAll(book => book.IsNew == false).Count;
            return count / _shopLibrary.Books.Count >= ShopLibrary.OldBooks;
        }
    }
}