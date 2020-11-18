using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shop.Infrastructure.EntityFramework
{
    [UsedImplicitly]
    public class BooksContextTimeFactory: IDesignTimeDbContextFactory<BooksContext>
    {
        private const string DefaultConnectionString =
            "Host=127.0.0.1;Username=lda-dima;Password=XeraeSd2ed32e4;Database=book_shop;";
        public static DbContextOptions<BooksContext> GetNpgsqlOptions([CanBeNull]string connectionString)
        {
            return new DbContextOptionsBuilder<BooksContext>()
                .UseNpgsql(connectionString ?? DefaultConnectionString, x =>
                {
                    x.MigrationsHistoryTable("_BSMigrationsHistory", BooksContext.DefaultSchemaName);
                })
                .Options;
        }
        public BooksContext CreateDbContext(string[] args)
        {
            return new BooksContext(GetNpgsqlOptions(null));
        }
    }
}