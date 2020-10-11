using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BookShop;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace ShopTests
{
    public class SystemTest
    {
        [Test]
        public void SaleTest()
        {
            var id = 1;
            var book = new Book(100, id, Book.Genre.Adventure, false);
            var mock = new Mock<IShopLibrary>();
            mock.Setup(a => a.SaleBook(id)).Returns(false);
            mock.Setup(a => a.FewBooksLeft()).Returns(false);
            mock.Setup(a => a.ALotOfOldBooks()).Returns(false);
            var sys = new MarketSystem(mock.Object);
            var outstr = new StringBuilder("");
            using (var sw = new StringWriter(outstr))
            {
                Console.SetOut(sw);
                sys.SaleBook(1);
                mock.Verify(a => a.FewBooksLeft(), Times.Never);
                outstr.ToString().Should().Be("");
                
                mock.Setup(a => a.SaleBook(id)).Returns(true);
                sys.SaleBook(1);
                mock.Verify(a => a.FewBooksLeft(), Times.Once);
                outstr.ToString().Should().Be($"Книга с id {id} продана\r\n");
                outstr.Clear();

                mock.Setup(a => a.FewBooksLeft()).Returns(true);
                sys.SaleBook(1);
                mock.Verify(a => a.ALotOfOldBooks(), Times.Once);
                outstr.ToString().Should().Be($"Заказ поставки системой\r\nКнига с id {id} продана\r\n");
                outstr.Clear();

                mock.Setup(a => a.FewBooksLeft()).Returns(false);
                mock.Setup(a => a.ALotOfOldBooks()).Returns(true);
                sys.SaleBook(1);
                outstr.ToString().Should().Be($"Заказ поставки системой\r\nКнига с id {id} продана\r\n");
                outstr.Clear();
            }
        }
        [Test]
        public void ReceptionTest()
        {
            var book1 = new Book(100, 1, Book.Genre.Adventure, false);
            var book2 = new Book(100, 2, Book.Genre.Adventure, false);
            var book3 = new Book(100, 3, Book.Genre.Adventure, false);
            var book4 = new Book(100, 4, Book.Genre.Adventure, false);
            var book5 = new Book(100, 4, Book.Genre.Adventure, false);
            
            var mock = new Mock<IShopLibrary>();
            mock.Setup(a => a.ReceptionBook(book1)).Returns(0);
            mock.Setup(a => a.ReceptionBook(book2)).Returns(-1);
            mock.Setup(a => a.ReceptionBook(book3)).Returns(-2);
            mock.Setup(a => a.ReceptionBook(book4)).Returns(-3);
            mock.Setup(a => a.ReceptionBook(book5)).Returns(-2);
            var sys = new MarketSystem(mock.Object);
            var outstr = new StringBuilder("");
            using (var sw = new StringWriter(outstr))
            {
                Console.SetOut(sw);
                sys.BookReception(new List<Book>(){book1, book2, book3, book4, book5});
                mock.Verify(a => a.ReceptionBook(book1), Times.Once);
                mock.Verify(a => a.ReceptionBook(book2), Times.Once);
                mock.Verify(a => a.ReceptionBook(book3), Times.Once);
                mock.Verify(a => a.ReceptionBook(book4), Times.Once);
                mock.Verify(a => a.ReceptionBook(book5), Times.Never);
                outstr.ToString().Should().Be("1 книг принято\r\n");
            }
        }
    }
}