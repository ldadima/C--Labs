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
        private readonly BooksContextDbContextFactory _dbContextFactory;

        public BooksController(BooksContextDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        
        [HttpGet]
        public async Task<List<Book>> GetBooks()
        {
            using (var context = _dbContextFactory.GetContext())
            {
                return await context.GetBooks();
            }
        }

        #warning если следовать REST идеологии, то тут не нужно указывать путь /add. 
        #warning post /api/book как бы предполагает, что это добавление книги
        [Route("add")]
        [HttpPost]
        public async Task AddBook([FromBody] Book book)
        {
            using (var context = _dbContextFactory.GetContext())
            {
                context.AddBook(book);
                await context.SaveChangesAsync();
            }
        }
    }
}