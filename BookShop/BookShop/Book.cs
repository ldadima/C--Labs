using System;
using System.Text.Json.Serialization;
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
        public string Genre { get; private set; }

        public bool IsNew { get; private set; }

        public DateTime DateDelivery { get; private set; }

        [JsonIgnore]
        public long ShopId { get; set; } = 1;
        [JsonIgnore]
        public ShopLibrary ShopLibrary { get; set; }

        private Book()
        {
        }
        
        public Book(long id, string title, string genre, bool isNew, double price, DateTime dateDelivery)
        {
            Id = id;
            Title = title;
            Genre = genre;
            IsNew = isNew;
            Price = price;
            CurrentPrice = Price;
            DateDelivery = dateDelivery;
        }

        public void ChangePrice(double percent)
        {
            if (Math.Abs(CurrentPrice - Price) == 0)
            {
                CurrentPrice *= 1 - percent;
            }
        }

        public void ReturnPrice()
        {
            CurrentPrice = Price;
        }
    }
}