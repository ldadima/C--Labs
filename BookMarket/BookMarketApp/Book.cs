using System;

namespace BookMarketApp
{
    public class Book
    {    
        public Guid Id { get;  }
        private double Price { get; }
        
        public double CurrentPrice { get; private set; }
        public enum Genre
        {
            Fantasy,
            Adventure,
            Encyclopedia
        }

        public Genre BookGenre { get; }

        public bool Novelty { get; }

        public Book(double price, Guid id, Genre genre, bool novelty)
        {
            Id = id;
            BookGenre = genre;
            this.Novelty = novelty;
            Price = price;
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