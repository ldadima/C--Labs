using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop
{
    public interface IShopLibrary
    {
        bool FewBooksLeft();

        bool ALotOfOldBooks();

        bool SaleBook(long id);

        int ReceptionBook(Book book);

        void BeginSale();

        void EndSale();
    }
    public class ShopLibrary: IShopLibrary
    {
        public List<Book> Books { get; } = new List<Book>();
        private readonly int _capacity;

        public ShopLibrary(int capacity, double balance)
        {
            this._capacity = capacity;
            Balance = balance;
        }
        
        public ShopLibrary(int capacity, double balance, List<Book> beginBooks)
        {
            this._capacity = capacity;
            Balance = balance;
            Books.AddRange(beginBooks);
        }

        public double Balance { private set; get; }

        public const double FantasySale = 0.03;
        public const double AdventureSale = 0.07;
        public const double EncyclopediaSale = 0.1;

        private const double PercentLeft = 0.1;
        private const double OldBooks = 0.75;

        private void AddBalance(double plus)
        {
            Balance += plus;
        }

        private bool ReduceBalance(double reduce)
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

        public bool SaleBook(long id)
        {
            var find = Books.FirstOrDefault(book => book.Id == id);
            if (find == null) return false;
            AddBalance(find.CurrentPrice);
            Books.Remove(find);
            return true;
        }

        public int ReceptionBook(Book book)
        {
            if (_capacity == Books.Count) return -3;
            if (Books.FirstOrDefault(bookL => bookL.Id == book.Id) != null) return -1;
            if (!ReduceBalance(book.CurrentPrice * 0.07)) return -2;
            Books.Add(book);
            return 0;
        }

        public void BeginSale()
        {
            foreach (var book in Books)
            {
                switch (book.BookGenre)
                {
                    case Book.Genre.Adventure:
                        book.ChangePrice(AdventureSale);
                        break;
                    case Book.Genre.Fantasy:
                        book.ChangePrice(FantasySale);
                        break;
                    default:
                        book.ChangePrice(EncyclopediaSale);
                        break;
                }
            }
        }

        public void EndSale()
        {
            foreach (var book in Books)
            {
                book.ReturnPrice();
            }
        }
    }
}