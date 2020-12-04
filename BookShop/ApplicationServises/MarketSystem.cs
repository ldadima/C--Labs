using System;
using System.Collections.Generic;
using System.Linq;
using BookShop;

namespace ApplicationServises
{
    public class MarketSystem
    {
        private readonly ShopLibrary _shop;

        public MarketSystem(ShopLibrary shop)
        {
            _shop = shop;
        }

        public void SaleBook(long id)
        {
            var find = _shop.Books.FirstOrDefault(book => book.Id == id);
            if (find == null)
            {
                throw new ArgumentException("Book not Found");
            }

            AddBalance(find.CurrentPrice);
            _shop.Books.Remove(find);
            if (IsNeedSomeBooks())
            {
                DeliveryRequest();
            }

            Console.WriteLine($"Книга с id {id} продана");
        }

        public bool IsNeedSomeBooks()
        {
            return IsFewBooksLeft() || HasManyOldBooks();
        }

        public void BookReception(IEnumerable<Book> books)
        {
            var count = 0;
            foreach (var book in books)
            {
                if (_shop.Capacity == _shop.Books.Count)
                {
                    throw new OutOfMemoryException("Storage of library is full");
                }

                if (_shop.Books.FirstOrDefault(bookL => bookL.Id == book.Id) != null)
                {
                    Console.WriteLine($"Книга с id {book.Id} уже есть");
                    continue;
                }

                if (!ReduceBalance(book.CurrentPrice * 0.07))
                {
                    Console.WriteLine($"На книгу c id {book.Id} по цене {book.CurrentPrice * 0.07} не хватает денег");
                    continue;
                }

                _shop.Books.Add(book);
                count++;
            }
            
            Console.WriteLine($"{count} книг принято");
        }

        private static void DeliveryRequest()
        {
            // something connect with BookDeliver
            Console.WriteLine("Заказ поставки системой");
        }

        public void BeginSale()
        {
            foreach (var book in _shop.Books)
            {
                switch (book.BookGenre)
                {
                    case "Adventure":
                        book.ChangePrice(ShopLibrary.AdventureSale);
                        break;
                    case "Fantasy":
                        book.ChangePrice(ShopLibrary.FantasySale);
                        break;
                    case "Encyclopedia":
                        book.ChangePrice(ShopLibrary.EncyclopediaSale);
                        break;
                }
            }
            
            Console.WriteLine("Старт акции");
        }

        public void EndSale()
        {
            foreach (var book in _shop.Books)
            {
                book.ReturnPrice();
            }

            Console.WriteLine("Конец акции");
        }


        private void AddBalance(double plus)
        {
            _shop.Balance += plus;
        }

        private bool ReduceBalance(double reduce)
        {
            if (reduce > _shop.Balance)
            {
                return false;
            }
            _shop.Balance -= reduce;
            return true;
        }

        private bool IsFewBooksLeft()
        {
            return _shop.Books.Count / (double) _shop.Capacity <= ShopLibrary.PercentLeft;
        }

        private bool HasManyOldBooks()
        {
            double count = _shop.Books.FindAll(book => book.IsNew == false).Count;
            return count / _shop.Books.Count >= ShopLibrary.OldBooks;
        }
    }
}