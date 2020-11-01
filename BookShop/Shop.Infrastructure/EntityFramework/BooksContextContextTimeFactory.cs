using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shop.Infrastructure.EntityFramework
{
    [UsedImplicitly]
    public class BooksContextContextTimeFactory
    {
        private const string DefaultConnectionString =
            "Data Source=127.0.0.1;Initial Catalog=BSTestApplication;User Id=sa; Password=2wsx2WSX;";
        public static DbContextOptions<BooksContext> GetSqlServerOptions([CanBeNull]string connectionString)
        {
            return new DbContextOptionsBuilder<BooksContext>()
                .UseSqlServer(connectionString ?? DefaultConnectionString, x =>
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