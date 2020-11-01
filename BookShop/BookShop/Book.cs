using System;

namespace BookShop
{
    public class Book
    {    
        public long Id { get;  }
        private double Price { get; }
        
        public double CurrentPrice { get; private set; }
        public Genre BookGenre { get; }

        public bool Novelty { get; }
        
        public long ShopId { get; set; }

        public ShopLibrary ShopLibrary { get; set; }

        public Book(double price, long id, Genre genre, bool novelty)
        {
            Id = id;
            BookGenre = genre;
            this.Novelty = novelty;
            Price = price;
            CurrentPrice = Price;
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