namespace CryptoBasket.Repository.Mappings
{
    using CryptoBasket.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Quantity)
                .HasColumnName("Quantity");

            builder.OwnsOne(p => p.Product, product =>
            {
                product.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("ProductId");

                product.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("ProductName");

                product.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnName("ProductPrice");

                product.Property(e => e.Symbol)
                    .IsRequired()
                    .HasColumnName("ProductSymbol");
            });
        }
    }
}
