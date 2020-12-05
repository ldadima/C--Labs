using ContractRabbit;

namespace WebShopApplication.Producer
{
    public class BookRequest: IBookRequest
    {
        public int BookCount { get; set; }

        public BookRequest(int bookCount)
        {
            BookCount = bookCount;
        }
    }
}