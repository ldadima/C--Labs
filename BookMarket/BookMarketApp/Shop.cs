using System.Collections.Generic;

namespace BookMarketApp
{
    public class Shop
    {
        public readonly List<Book> Books = new List<Book>();
        private readonly int _capacity;

        public Shop(int capacity)
        {
            this._capacity = capacity;
        }

        private double Balance { set; get; }
        
        public const double FantasySale = 0.03;
        public const double AdventureSale = 0.07;
        public const double EncyclopediaSale = 0.1;

        private const double PercentLeft = 0.1;
        private const double OldBooks = 0.75;

        public void AddBalance(double plus)
        {
            Balance += plus;
        }

        public bool ReduceBalance(double reduce)
        {
            if (reduce > Balance) return false;
            Balance -= reduce;
            return true;

        }

        public bool FewBooksLeft()
        {
            return Books.Count / (double) _capacity <= PercentLeft;
        }
        
        public bool ALotOfOldBooks()
        {
            double count = Books.FindAll(book => book.Novelty==false).Count;
            return count / Books.Count >= OldBooks;
        }

        public bool AddBook(Book book)
        {
            if (_capacity == Books.Count) return false;
            Books.Add(book);
            return true;
        }
    }
}