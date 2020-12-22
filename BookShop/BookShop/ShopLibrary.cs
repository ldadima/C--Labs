using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace BookShop
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ShopLibrary
    {
        public long Id { get; set; }
        public int Capacity{ get; set; }
        public double Balance { get; private set; }
        public List<Book> Books { get; private set; }

        private ShopLibrary()
        {
            
        }
        public ShopLibrary(int capacity)
        {
            Capacity = capacity;
        }
        
        public const double FictionSale = 0.03;
        public const double AdventureSale = 0.07;
        public const double EncyclopediaSale = 0.1;

        public const double PercentLeft = 0.1;
        public const double OldBooks = 0.75;

        public void DeleteBook(long id)
        {
            Books = Books.Where(b => b.Id != id).ToList();
        }
    }
}