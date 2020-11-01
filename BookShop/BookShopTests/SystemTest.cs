using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BookShop;
using FluentAssertions;
using NUnit.Framework;
using Moq;
using NFluent;

namespace ShopTests
{
    public class SystemTest
    {
        [Test]
        public void SaleBookTest()
        {
            var outstr = new StringBuilder("");
            using (var sw = new StringWriter(outstr))
            {
                var id = 1;
                var book = new Book(100, id, Genre.Adventure, false);
                var library = new ShopLibrary(11);
                var sys = new MarketSystem(library);
                var rTextWriter = Console.Out;
                Console.SetOut(sw);
                Check.ThatCode(() => sys.SaleBook(id))
                    .Throws<ArgumentException>()
                    .WithMessage("Book not Found");
                
                library.Books.AddRange(new []
                {
                    book
                });
                sys.SaleBook(id);
                outstr.ToString().Should().Be($"Заказ поставки системой\r\nКнига с id {id} продана\r\n");
                outstr.Clear();
                
                library.Books.AddRange(new[]
                {
                    book,
                    new Book(20, 2, Genre.Adventure, false),
                    new Book(20, 3, Genre.Adventure, false),
                    new Book(20, 4, Genre.Adventure, false),
                });
                sys.SaleBook(id);
                outstr.ToString().Should().Be($"Заказ поставки системой\r\nКнига с id {id} продана\r\n");
                outstr.Clear();
                
                library.Books.AddRange(new []{
                    book,
                    new Book(20, 6,Genre.Adventure, true),
                    new Book(20, 8,Genre.Adventure, true),
                    new Book(20, 9,Genre.Adventure, true)});
                sys.SaleBook(id);
                outstr.ToString().Should().Be($"Книга с id {id} продана\r\n");
                library.Balance.Should().Be(book.CurrentPrice * 3);
                outstr.Clear();
                
                Console.SetOut(rTextWriter);
            }
        }
        [Test]
        public void ReceptionTest()
        {
            var book1 = new Book(100, 1, Genre.Adventure, false);
            var book2 = new Book(1000, 2, Genre.Adventure, false);
            var book3 = new Book(100, 1, Genre.Adventure, false);
            var book4 = new Book(100, 3, Genre.Adventure, false);
            var book5 = new Book(100, 4, Genre.Adventure, false);
            
            var library = new ShopLibrary(2) {Balance = 50};
            var sys = new MarketSystem(library);
            var outstr = new StringBuilder("");
            using (var sw = new StringWriter(outstr))
            {
                var rWriter = Console.Out;
                Console.SetOut(sw);
                sys.BookReception(new List<Book>(){book1, book2, book3, book4});
                outstr.ToString().Should().Contain("2 книг принято");
                outstr.ToString().Should().Contain($"На книгу c id {book2.Id} по цене {book2.CurrentPrice*0.07} не хватает денег");
                outstr.ToString().Should().Contain($"Книга с id {book3.Id} уже есть");
                Check.ThatCode(() => sys.BookReception(new List<Book>(){book5}))
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
                new Book(100, 1,Genre.Fantasy, true),
                new Book(100, 2,Genre.Adventure, true),
                new Book(100, 3,Genre.Encyclopedia, true)
            };
            var tmpShop = new ShopLibrary(1) {Balance = 50,Books = books};
            var system = new MarketSystem(tmpShop);
            system.BeginSale();
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
            system.EndSale();
            foreach (var book in tmpShop.Books)
            {
                book.CurrentPrice.Should().Be(100); break;
            }
        }

    }
}