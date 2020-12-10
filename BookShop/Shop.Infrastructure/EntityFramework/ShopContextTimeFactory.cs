using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shop.Infrastructure.EntityFramework
{
    [UsedImplicitly]
    public class ShopContextTimeFactory: IDesignTimeDbContextFactory<ShopContext>
    {
        private const string DefaultConnectionString =
            "Host=127.0.0.1;Username=lda-dima;Password=XeraeSd2ed32e4;Database=book_shop;";
        public static DbContextOptions<ShopContext> GetNpgsqlOptions([CanBeNull]string connectionString)
        {
            return new DbContextOptionsBuilder<ShopContext>()
                .UseNpgsql(connectionString ?? DefaultConnectionString, x =>
                {
                    x.MigrationsHistoryTable("_BSMigrationsHistory", ShopContext.DefaultSchemaName);
                })
                .Options;
        }
        public ShopContext CreateDbContext(string[] args)
        {
            return new ShopContext(GetNpgsqlOptions(null));
        }
    }
}