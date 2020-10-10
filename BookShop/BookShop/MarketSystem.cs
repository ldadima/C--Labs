using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop
{
    public class MarketSystem
    {
        private readonly Shop _shop;

        public MarketSystem(int shopCapacity)
        {
            _shop = new Shop(shopCapacity);
        }

        public void SaleBook(Guid id)
        {
            var find = _shop.Books.FirstOrDefault(book => book.Id == id);
            if (find == null) return;
            _shop.Books.Remove(find);
            _shop.AddBalance(find.CurrentPrice);
            if (_shop.FewBooksLeft() || _shop.ALotOfOldBooks())
            {
                DeliveryRequest();
            }
            Console.WriteLine($"Книга с id {id} продана");
        }

        public void BookReception(IEnumerable<Book> books)
        {
            var count = 0;
            foreach (var book in books)
            {
                if (!_shop.ReduceBalance(book.CurrentPrice*7/100)) break;
                _shop.AddBook(book);
                count++;
            }
            Console.WriteLine($"{count} книг принято");

        }

        public void DeliveryRequest()
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
                    case Book.Genre.Adventure:
                        book.ChangePrice(Shop.AdventureSale);
                        break;
                    case Book.Genre.Fantasy:
                        book.ChangePrice(Shop.FantasySale);
                        break;
                    default:
                        book.ChangePrice(Shop.EncyclopediaSale);
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
    }
}