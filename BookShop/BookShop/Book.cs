using System;
using JetBrains.Annotations;

namespace BookShop
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Book
    {    
        public long Id { get; private set; }
        
        public string Title { get; private set; }
        public double Price { get; private set; }
        
        public double CurrentPrice { get; private set; }
        public string BookGenre { get; private set; }

        public bool IsNew { get; private set; }

        public DateTime DateDelivery { get; private set; }

        private Book()
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