using System;

namespace BookShop
{
    public class Book
    {    
        #warning Book - это у тебя доменная сущность, о целостности этих данных нужно заботиться. 
        #warning следуя одному из принципов ООП, у доменных сущностей сеттеры лучше делать приватными
        public long Id { get; set; }
        
        public string Title { get; set; }
        public double Price { get; set; }
        
        public double CurrentPrice { get; private set; }
        public string BookGenre { get; set; }

        public bool IsNew { get; set; }

        public DateTime DateDelivery { get; set; }
        
        #warning закоменченный код — зло :) страдает чистота кода, а иногда может вызывать вопросы, зачем этот кусок тут вообще нужен
        
        // public long ShopId { get; set; }

        // public ShopLibrary ShopLibrary { get; set; }

        #warning можно на весь класс навесить атрибут [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)] из пакета JetBrains.Annotations, чтобы этот конструктор не подсвечивался неиспользуемым
        #warning а т.к. нужен он только для EFa, то его можно сделать приватным
        public Book()
        {
            
        }

        public Book(long id, string genre, bool isNew, double price, DateTime dateDelivery)
        {
            Id = id;
            BookGenre = genre;
            IsNew = isNew;
            Price = price;
            CurrentPrice = Price;
            DateDelivery = dateDelivery;
        }

        public void ChangePrice(double percent)
        {
            CurrentPrice *= 1 - percent;
        }

        public void ReturnPrice()
        {
            CurrentPrice = Price;
        }
    }
}