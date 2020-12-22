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

        public async Task<ShopLibrary> GetShopLibrary()
        {
            return await Set<ShopLibrary>()
                .Include(s => s.Books)
                .FirstOrDefaultAsync();
        }

        public void UpdateShopLibrary(ShopLibrary shop)
        {
            Set<ShopLibrary>().Update(shop);
        }
    }
}