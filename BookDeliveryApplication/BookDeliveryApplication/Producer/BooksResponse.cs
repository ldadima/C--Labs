using System.Collections.Generic;
using ContractRabbit;

namespace BookDeliveryApplication.Producer
{
    public class BooksResponse: IBookResponse
    {
        public List<IBookResponse.Book> Books { get; set; }
    }
}