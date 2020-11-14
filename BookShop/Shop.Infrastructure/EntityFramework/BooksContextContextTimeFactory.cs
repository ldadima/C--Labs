using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.EntityFramework
{
    [UsedImplicitly]
    public class BooksContextContextTimeFactory
    {
        private const string DefaultConnectionString =
            "Data Source=127.0.0.1;Initial Catalog=book_shop;User Id=lda-dima; Password=Xer$##aeSd2ed32$e4;";
        public static DbContextOptions<BooksContext> GetSqlServerOptions([CanBeNull]string connectionString)
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
            return new BooksContext(GetSqlServerOptions(null));
        }
    }
}