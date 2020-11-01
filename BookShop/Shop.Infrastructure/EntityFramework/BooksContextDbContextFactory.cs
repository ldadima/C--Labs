namespace Shop.Infrastructure.EntityFramework
{
    public sealed class BooksContextDbContextFactory
    {
        private readonly string _connectionString;

        public BooksContextDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BooksContext GetContext()
        {
            return new BooksContext(BooksContextContextTimeFactory.GetSqlServerOptions(_connectionString));
        }
    }
}