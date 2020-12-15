using System.Collections.Generic;
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
        public List<Book> GetBooks()
        {
            return _marketSystem.GetBooks();
        }

        [HttpPut]
        public void SaleBook([FromBody] Book book)
        {
            
        }
    }
}