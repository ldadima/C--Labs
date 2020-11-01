using System.Collections.Generic;

namespace BookShop
{
    public class ShopLibrary
    {
        public long Id { get; set; }
        public readonly int Capacity;
        public double Balance { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        public ShopLibrary(int capacity)
        {
            Capacity = capacity;
        }
        
        public const double FantasySale = 0.03;
        public const double AdventureSale = 0.07;
        public const double EncyclopediaSale = 0.1;

        public const double PercentLeft = 0.1;
        public const double OldBooks = 0.75;
        
    }
}