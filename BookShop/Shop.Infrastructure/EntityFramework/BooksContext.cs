using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.EntityFramework
{
    public class BooksContext: DbContext
    {
        public const string DefaultSchemaName = "Books";

        public BooksContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
        }

        public async Task<List<Book>> GetPersons()
        {
            return await Set<Book>()
                .ToListAsync();
        }

        public void AddPerson(Book book)
        {
            Set<Book>().Add(book);
        }
    }
}