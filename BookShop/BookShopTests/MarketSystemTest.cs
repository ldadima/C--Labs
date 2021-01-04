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
            var book = new Book(id, "adventure","adventure", false, 100, DateTime.Today);
            var library = new ShopLibrary(1, 11, 1000);
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
                new Book(2, "adventure","adventure", false, 20, DateTime.Today),
                new Book(3, "adventure","adventure", false, 20, DateTime.Today),
                new Book(4, "adventure","adventure", false, 20, DateTime.Today)
            });
            res = sys.IsNeedSomeBooks();
            res.Should().BeTrue();

            library.Books.AddRange(new[]
            {
                book,
                new Book(6, "adventure","adventure", true, 20, DateTime.Today),
                new Book(7, "adventure","adventure", true, 20, DateTime.Today),
                new Book(8, "adventure","adventure", true, 20, DateTime.Today)
            });
            res = sys.IsNeedSomeBooks();
            res.Should().BeFalse();
        }

        [Test]
        public void SaleBookTest()
        {
            var id = 1;
            var book = new Book(id, "adventure","adventure", false, 100, DateTime.Today);
            var library = new ShopLibrary(1, 11, 100);
            Check.ThatCode(() => library.SaleBook(id))
                .Throws<KeyNotFoundException>()
                .WithMessage($"Book with id - {id} not found");

            library.Books.AddRange(new[]
            {
                book
            });
            library.SaleBook(id);
            library.Balance.Should().Be(200);
        }

        [Test]
        public void ReceptionTest()
        {
            var book1 = new Book(1, "adventure","adventure", false, 100, DateTime.Today);
            var book2 = new Book(2, "adventure","adventure", false, 1000, DateTime.Today);
            var book3 = new Book(3, "adventure","adventure", false, 100, DateTime.Today);
            var book4 = new Book(4, "adventure","adventure", false, 100, DateTime.Today);

            var library = new ShopLibrary(1, 2, 50);

            var outstr = new StringBuilder("");
            using (var sw = new StringWriter(outstr))
            {
                var rWriter = Console.Out;
                Console.SetOut(sw);
                library.TryAddBook(book1).Should().BeTrue();
                library.TryAddBook(book2).Should().BeFalse();
                library.TryAddBook(book3).Should().BeTrue();
                outstr.ToString().Should()
                    .Contain($"На книгу c id {book2.Id} по цене {book2.CurrentPrice * 0.07} не хватает денег");

                Check.ThatCode(() => library.TryAddBook(book4))
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
                new Book(1, "adventure","fiction", true, 100, DateTime.Today),
                new Book(2, "adventure","adventure", true, 100, DateTime.Today),
                new Book(3, "adventure","encyclopedia", true, 100, DateTime.Today)
            };
            var tmpShop = new ShopLibrary(1, 3, 100);
            tmpShop.Books.AddRange(books);
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