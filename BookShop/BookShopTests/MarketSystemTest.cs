using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BookShop;
using FluentAssertions;
using NFluent;
using NUnit.Framework;
using WebShopApplication.Services;

namespace ShopTests
{
    public class MarketSystemTest
    {
        [Test]
        public void IsNeedBooksTest()
        {
            var id = 1;
            var book = new Book(id, "adventure", false, 100, DateTime.Today);
            var library = new ShopLibrary(11);
            library.Books = new List<Book>();
            var sys = new MarketSystem(library);
            library.Books.AddRange(new[]
            {
                book
            });
            var res = sys.IsNeedSomeBooks();
            res.Should().BeTrue();

            library.Books.AddRange(new[]
            {
                book,
                new Book(2, "adventure", false, 20, DateTime.Today),
                new Book(3, "adventure", false, 20, DateTime.Today),
                new Book(4, "adventure", false, 20, DateTime.Today)
            });
            res = sys.IsNeedSomeBooks();
            res.Should().BeTrue();

            library.Books.AddRange(new[]
            {
                book,
                new Book(6, "adventure", true, 20, DateTime.Today),
                new Book(7, "adventure", true, 20, DateTime.Today),
                new Book(8, "adventure", true, 20, DateTime.Today)
            });
            res = sys.IsNeedSomeBooks();
            res.Should().BeFalse();
        }

        [Test]
        public void SaleBookTest()
        {
            var outstr = new StringBuilder("");
            using (var sw = new StringWriter(outstr))
            {
                var id = 1;
                var book = new Book(id, "adventure", false, 100, DateTime.Today);
                var library = new ShopLibrary(11);
                library.Books = new List<Book>();
                var sys = new MarketSystem(library);
                var rTextWriter = Console.Out;
                Console.SetOut(sw);
                Check.ThatCode(() => sys.SaleBook(id).GetAwaiter().GetResult())
                    .Throws<ArgumentException>()
                    .WithMessage("Book not Found");

                library.Books.AddRange(new[]
                {
                    book
                });
                sys.SaleBook(id).GetAwaiter().GetResult();
                outstr.ToString().Should().Be($"Книга с id {id} продана\r\n");
                outstr.Clear();
                
                Console.SetOut(rTextWriter);
            }
        }

        [Test]
        public void ReceptionTest()
        {
            var book1 = new Book(1, "adventure", false, 100, DateTime.Today);
            var book2 = new Book(2, "adventure", false, 1000, DateTime.Today);
            var book3 = new Book(1, "adventure", false, 100, DateTime.Today);
            var book4 = new Book(3, "adventure", false, 100, DateTime.Today);
            var book5 = new Book(4, "adventure", false, 100, DateTime.Today);

            var library = new ShopLibrary(2) {Balance = 50};
            library.Books = new List<Book>();
            var sys = new MarketSystem(library);
            var outstr = new StringBuilder("");
            using (var sw = new StringWriter(outstr))
            {
                var rWriter = Console.Out;
                Console.SetOut(sw);
                sys.BookReception(new List<Book>() {book1, book2, book3, book4}).GetAwaiter().GetResult();
                outstr.ToString().Should().Contain("2 книг принято");
                outstr.ToString().Should()
                    .Contain($"На книгу c id {book2.Id} по цене {book2.CurrentPrice * 0.07} не хватает денег");
                outstr.ToString().Should().Contain($"Книга с id {book3.Id} уже есть");
                Check.ThatCode(() => sys.BookReception(new List<Book>() {book5}).GetAwaiter().GetResult())
                    .Throws<OutOfMemoryException>()
                    .WithMessage("Storage of library is full");

                Console.SetOut(rWriter);
            }
        }

        [Test]
        public void SaleTest()
        {
            var books = new List<Book>()
            {
                new Book(1, "fiction", true, 100, DateTime.Today),
                new Book(2, "adventure", true, 100, DateTime.Today),
                new Book(3, "encyclopedia", true, 100, DateTime.Today)
            };
            var tmpShop = new ShopLibrary(1) {Balance = 50, Books = books};
            var system = new MarketSystem(tmpShop);
            system.BeginSale();
            foreach (var book in tmpShop.Books)
            {
                switch (book.Id)
                {
                    case 1:
                        book.CurrentPrice.Should().Be(97);
                        break;
                    case 2:
                        book.CurrentPrice.Should().Be(93);
                        break;
                    case 3:
                        book.CurrentPrice.Should().Be(90);
                        break;
                }
            }

            system.EndSale();
            foreach (var book in tmpShop.Books)
            {
                book.CurrentPrice.Should().Be(100);
                break;
            }
        }
    }
}