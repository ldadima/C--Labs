using BookShop;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.EntityFramework.Configurations
{
    [UsedImplicitly]
    public class ShopConfiguration: IEntityTypeConfiguration<ShopLibrary>
    {
        public void Configure(EntityTypeBuilder<ShopLibrary> builder)
        {
            builder.ToTable(nameof(ShopLibrary), ShopContext.DefaultSchemaName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Balance);

            builder.HasMany(x => x.Books)
                .WithOne(b => b.ShopLibrary)
                .HasForeignKey(t => t.ShopId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}