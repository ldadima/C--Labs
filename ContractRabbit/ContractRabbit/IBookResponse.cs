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
            public long Id { get; private set; }
        
            public string Title { get; private set; }
            public double Price { get; private set; }
        
            public double CurrentPrice { get; private set; }
            public string BookGenre { get; private set; }

            public bool IsNew { get; private set; }

            public DateTime DateDelivery { get; private set; }
        }
    }
}