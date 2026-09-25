using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CampoSystem.ErpAI.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        var nameConverter = new ValueConverter<ProductName, string>(
            v => v.Name,
            v => new ProductName(v));

        builder.Property(p => p.Name)
               .HasConversion(nameConverter);

        var skuConverter = new ValueConverter<ProductSku, string>(
            v => v.Sku,
            v => new ProductSku(v));

        builder.Property(p => p.Sku)
               .HasConversion(skuConverter);

        var priceConverter = new ValueConverter<Money, decimal>(
            v => v.Amount,
            v => Money.From(v));

        builder.Property(p => p.Price)
               .HasConversion(priceConverter);

        builder.Property(p => p.Description)
               .HasMaxLength(1000);

        builder.Property(p => p.IsActive)
               .IsRequired();

        builder.Property(p => p.CreatedAt)
               .IsRequired();
    }
}
