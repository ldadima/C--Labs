using System.Collections.Generic;
using BookShop;
using FluentAssertions;
using NUnit.Framework;

namespace ShopTests
{
    public class Tests
    {
        [Test]
        public void FewBookTest()
        {
            var books = new List<Book>()
            {
                new Book(20, 1,Book.Genre.Adventure, true)
            };
            var tmpShop = new ShopLibrary(11, 50, books);
            tmpShop.FewBooksLeft().Should().BeTrue();
            books.AddRange(new []
            {
                new Book(20, 2,Book.Genre.Adventure, false),
                new Book(20, 3,Book.Genre.Adventure, false)
            });
            tmpShop = new ShopLibrary(11, 50, books);
            tmpShop.FewBooksLeft().Should().BeFalse();
        }
                
        [Test]
        public void ALotOfBookTest()
        {
            var books = new List<Book>()
            {
                new Book(20, 1,Book.Genre.Adventure, true)
            };
            var tmpShop = new ShopLibrary(11, 50, books);
            tmpShop.ALotOfOldBooks().Should().BeFalse();
            books.AddRange(new []
            {
                new Book(20, 2,Book.Genre.Adventure, false),
                new Book(20, 3,Book.Genre.Adventure, false),
                new Book(20, 4,Book.Genre.Adventure, false),
                new Book(20, 5,Book.Genre.Adventure, false),
                new Book(20, 7,Book.Genre.Adventure, false),
                new Book(20, 6,Book.Genre.Adventure, false),
                new Book(20, 8,Book.Genre.Adventure, false),
                new Book(20, 9,Book.Genre.Adventure, false)
            });
            tmpShop = new ShopLibrary(11, 50, books);
            tmpShop.ALotOfOldBooks().Should().BeTrue();
        }

        [Test]
        public void SaleBookTest()
        {
            int id = 1;
            var books = new List<Book>()
            {
                new Book(20, id,Book.Genre.Adventure, true)
            };
            var tmpShop = new ShopLibrary(11, 50, books);
            tmpShop.SaleBook(id).Should().BeTrue();
            tmpShop.Books.Count.Should().Be(0); 
            
            tmpShop = new ShopLibrary(11, 50, books);
            tmpShop.SaleBook(2).Should().BeFalse();
            tmpShop.Books.Count.Should().Be(1);
        }

        [Test]
        public void ReceptionTest()
        {
            var books = new List<Book>()
            {
                new Book(20, 1,Book.Genre.Adventure, true)
            };
            var tmpShop = new ShopLibrary(1, 50, books);
            tmpShop.ReceptionBook(new Book(2, 3, Book.Genre.Encyclopedia, true)).Should().Be(-3);
            tmpShop.Books.Count.Should().Be(1);
            tmpShop = new ShopLibrary(2, 50, books);
            tmpShop.ReceptionBook(new Book(55, 1, Book.Genre.Encyclopedia, true)).Should().Be(-1);
            tmpShop.Books.Count.Should().Be(1);
            tmpShop = new ShopLibrary(2, 2, books);
            tmpShop.ReceptionBook(new Book(55, 2, Book.Genre.Encyclopedia, true)).Should().Be(-2);
            tmpShop.Books.Count.Should().Be(1);
            tmpShop = new ShopLibrary(2, 50, books);
            tmpShop.ReceptionBook(new Book(55, 2, Book.Genre.Encyclopedia, true)).Should().Be(0);
            tmpShop.Books.Count.Should().Be(2);
        }

        [Test]
        public void SaleTest()
        {
            var books = new List<Book>()
            {
                new Book(100, 1,Book.Genre.Fantasy, true),
                new Book(100, 2,Book.Genre.Adventure, true),
                new Book(100, 3,Book.Genre.Encyclopedia, true)
            };
            var tmpShop = new ShopLibrary(1, 50, books);
            tmpShop.BeginSale();
            foreach (var book in tmpShop.Books)
            {
                switch (book.Id)
                {
                    case 1:
                        book.CurrentPrice.Should().Be(97); break;
                    case 2:
                        book.CurrentPrice.Should().Be(93); break;
                    case 3:
                        book.CurrentPrice.Should().Be(90); break;
                }
            }
            tmpShop.EndSale();
            foreach (var book in tmpShop.Books)
            {
                book.CurrentPrice.Should().Be(100); break;
            }
        }
        
    }
}