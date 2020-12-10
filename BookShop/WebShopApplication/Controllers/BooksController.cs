using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop;
using Microsoft.AspNetCore.Mvc;
using Shop.Infrastructure.EntityFramework;

namespace WebShopApplication.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ShopContextDbContextFactory _dbContextFactory;

        public BooksController(ShopContextDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        
        [HttpGet]
        public async Task<List<ShopLibrary>> GetBooks()
        {
            using (var context = _dbContextFactory.GetContext())
            {
                return await context.GetShopLibrary();
            }
        }

        [HttpPost]
        public async Task AddBook([FromBody] ShopLibrary shop)
        {
            using (var context = _dbContextFactory.GetContext())
            {
                context.AddShopLibrary(shop);
                await context.SaveChangesAsync();
            }
        }
    }
}