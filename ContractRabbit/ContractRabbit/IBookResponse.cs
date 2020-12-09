using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ContractRabbit
{
    public interface IBookResponse
    {
        List<Book> Books { get; set; }

        #warning интерфейс у тебя public, а пропертя Books и сам класс без модификаторов, значит, по умолчанию, они приватные
        #warning если бы ты попровал создать объект класса-реализатора этого интерефейса, то у тебя бы не получилось проиниициализировать эти свойства
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        class Book
        {
            #warning ну и даже если бы пропертя и класс были паблик, то ты бы не смог задать эти поля, тк они у тебя приватные
            #warning а никакого конструктора тут нет (да и быть не может)
            #warning кажется, что ты опять не проверял свой код :) 
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