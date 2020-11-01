﻿using System;
 using System.Collections.Generic;
 using System.Linq;

 namespace BookShop
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
            if (FewBooksLeft() || ALotOfOldBooks())
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
                    Console.WriteLine($"На книгу с ценой {book.CurrentPrice} не хватает денег");
                    continue;
                }
                _shop.Books.Add(book);
                count++;
            }
            Console.WriteLine($"{count} книг принято");

        }

        private void DeliveryRequest()
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
                    case Genre.Adventure:
                        book.ChangePrice(ShopLibrary.AdventureSale);
                        break;
                    case Genre.Fantasy:
                        book.ChangePrice(ShopLibrary.FantasySale);
                        break;
                    case Genre.Encyclopedia:
                        book.ChangePrice(ShopLibrary.EncyclopediaSale);
                        break;
                    default:
                        throw new ArgumentException("Error with Genre of book");
                }
            }            Console.WriteLine("Старт акции");
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
            if (reduce > _shop.Balance) return false;
            _shop.Balance -= reduce;
            return true;

        }

        private bool FewBooksLeft()
        {
            return _shop.Books.Count / (double) _shop.Capacity <= ShopLibrary.PercentLeft;
        }
        
        private bool ALotOfOldBooks()
        {
            double count = _shop.Books.FindAll(book => book.Novelty==false).Count;
            return count / _shop.Books.Count >= ShopLibrary.OldBooks;
        }
    }
}