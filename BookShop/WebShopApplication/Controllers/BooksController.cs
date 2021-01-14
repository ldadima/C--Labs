using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop;
using Microsoft.AspNetCore.Mvc;
using WebShopApplication.Services;

namespace WebShopApplication.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MarketSystem _marketSystem;

        public BooksController(MarketSystem marketSystem)
        {
            _marketSystem = marketSystem;
        }
        
        [HttpGet]
        public async Task<List<Book>> GetBooks()
        {
            return await _marketSystem.GetBooks();
        }

        [HttpPut]
        [Route("/api/books/{id}")]
        public async Task<string> SaleBook(long id)
        {
           var book = await _marketSystem.SaleBook(id);
           return $"Вы купили книгу {book.Title}";
        }

        [HttpPut]
        [Route("/api/books/startSale")]
        public async Task StartSale()
        {
            await _marketSystem.BeginSale();
        }

        [HttpPut]
        [Route("/api/books/endSale")]
        public async Task EndSale()
        {
            await _marketSystem.EndSale();
        }

    }
}