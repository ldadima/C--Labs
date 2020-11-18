using System;
using BookShop;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;    
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.EntityFramework.Configurations
{
    [UsedImplicitly]
    public class BookConfiguration: IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book), BooksContext.DefaultSchemaName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title);
            builder.Property(x => x.BookGenre);
            builder.Property(x => x.IsNew);
            builder.Property(x => x.Price);
            builder.Property(x => x.DateDelivery);
        }
    }
}