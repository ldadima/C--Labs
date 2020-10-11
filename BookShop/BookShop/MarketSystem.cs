﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop
{
    public class MarketSystem
    {
        private readonly IShopLibrary _shop;

        public MarketSystem(IShopLibrary shop)
        {
            _shop = shop;
        }

        public void SaleBook(long id)
        {
            if(!_shop.SaleBook(id)) return;
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
                var err = _shop.ReceptionBook(book);
                if(err < -2) break;
                if (err == 0) count++;
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
            _shop.BeginSale();
            Console.WriteLine("Старт акции");
        }
        
        public void EndSale()
        {
            _shop.EndSale();
            Console.WriteLine("Конец акции");
        }
    }
}