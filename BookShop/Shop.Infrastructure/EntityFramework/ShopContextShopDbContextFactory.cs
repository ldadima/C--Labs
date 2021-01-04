
namespace Shop.Infrastructure.EntityFramework
{
    public sealed class ShopContextDbContextFactory
    {
        private readonly string _connectionString;

        public ShopContextDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ShopContext GetContext()
        {
            return new ShopContext(ShopContextTimeFactory.GetNpgsqlOptions(_connectionString));
        }
    }
}