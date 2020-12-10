using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.EntityFramework
{
    public class ShopContext: DbContext
    {
        public const string DefaultSchemaName = "public";

        public ShopContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
        }

        public async Task<List<ShopLibrary>> GetShopLibrary()
        {
            return await Set<ShopLibrary>()
                .ToListAsync();
        }

        public void AddShopLibrary(ShopLibrary shop)
        {
            Set<ShopLibrary>().Add(shop);
        }
    }
}