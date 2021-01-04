using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace BookShop
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ShopLibrary
    {
        public long Id { get; private set; }
        public int Capacity{ get; private set; }
        public double Balance { get; private set; }
        public List<Book> Books { get; private set; } = new List<Book>();

        private ShopLibrary()
        {
            
        }
        public ShopLibrary(long id , int capacity, double balance)
        {
            Id = id;
            Capacity = capacity;
            Balance = balance;
        }
        
        public const double FictionSale = 0.03;
        public const double AdventureSale = 0.07;
        public const double EncyclopediaSale = 0.1;

        public const double PercentLeft = 0.1;
        public const double OldBooks = 0.75;

        public Book SaleBook(long id)
        {
            var book = Books.FirstOrDefault(book1 => book1.Id == id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with id - {id} not found");
            }
            if (Books.Remove(book))
            {
                Balance += book.CurrentPrice;
            }

            return book;
        }

        public bool TryAddBook(Book book)
        {
            if (Capacity == Books.Count)
            {
                throw new OutOfMemoryException("Storage of library is full");
            }

            if (!TryReduceBalance(book.CurrentPrice * 0.07))
            {
                Console.WriteLine($"На книгу c id {book.Id} по цене {book.CurrentPrice * 0.07} не хватает денег");
                return false;
            }

            Books.Add(book);
            return true;
        }

        private bool TryReduceBalance(double reduce)
        {
            if (reduce > Balance)
            {
                return false;
            }

            Balance -= reduce;
            return true;
        }
        
        public bool IsFewBooksLeft()
        {
            return Books.Count / (double) Capacity <= PercentLeft;
        }

        public bool HasManyOldBooks()
        {
            double count = Books.FindAll(book => book.IsNew == false).Count;
            return count / Books.Count >= OldBooks;
        }
        
    }
}