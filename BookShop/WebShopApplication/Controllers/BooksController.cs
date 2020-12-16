using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.EntityFramework;
using WebShopApplication.Services;

namespace WebShopApplication.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MarketSystem _marketSystem;
        private readonly ShopContextDbContextFactory _dbContextFactory;

        public BooksController(ShopContextDbContextFactory dbContextFactory, MarketSystem marketSystem)
        {
            _marketSystem = marketSystem;
            _dbContextFactory = dbContextFactory;
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