using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ContractRabbit
{
    public interface IBookResponse
    {
        List<Book> Books { get; set; }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        class Book
        {
            public long Id { get; set; }
        
            public string Title { get; set; }
            public double Price { get; set; }
        
            public double CurrentPrice { get; set; }
            public string BookGenre { get; set; }

            public bool IsNew { get; set; }

            public DateTime DateDelivery { get; set; }
        }
    }
}