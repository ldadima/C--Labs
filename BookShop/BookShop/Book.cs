using System;

namespace BookShop
{
    public class Book
    {    
        public long Id { get; set; }
        
        public string Title { get; set; }
        public double Price { get; set; }
        
        public double CurrentPrice { get; private set; }
        public string BookGenre { get; set; }

        public bool IsNew { get; set; }

        public DateTime DateDelivery { get; set; }
        
        // public long ShopId { get; set; }

        // public ShopLibrary ShopLibrary { get; set; }

        public Book()
        {
            
        }

        public Book(long id, string genre, bool isNew, double price, DateTime dateDelivery)
        {
            Id = id;
            BookGenre = genre;
            IsNew = isNew;
            Price = price;
            CurrentPrice = Price;
            DateDelivery = dateDelivery;
        }

        public void ChangePrice(double percent)
        {
            CurrentPrice *= 1 - percent;
        }

        public void ReturnPrice()
        {
            CurrentPrice = Price;
        }
    }
}