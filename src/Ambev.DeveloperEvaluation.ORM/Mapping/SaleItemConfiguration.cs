using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        // Foreign Key to Sale
        builder.Property(si => si.SaleId).IsRequired().HasColumnType("uuid");

        // External Identity: Product (denormalized)
        builder.Property(si => si.ProductId).IsRequired().HasColumnType("uuid");
        builder.Property(si => si.ProductName).IsRequired().HasMaxLength(200);
        builder.Property(si => si.ProductSku).IsRequired().HasMaxLength(50);

        // Item data
        builder.Property(si => si.Quantity).IsRequired();
        builder.Property(si => si.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(si => si.Discount).IsRequired().HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(si => si.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");

        // Audit
        builder.Property(si => si.CreatedAt).IsRequired();
        builder.Property(si => si.UpdatedAt);

        // Indexes for better query performance
        builder.HasIndex(si => si.SaleId);
        builder.HasIndex(si => si.ProductId);
    }
}