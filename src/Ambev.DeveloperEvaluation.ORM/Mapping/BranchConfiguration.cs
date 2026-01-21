using Ambev.DeveloperEvaluation.Domain. Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft. EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;
public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Code).IsRequired().HasMaxLength(50);
        builder.Property(b => b.Address).HasMaxLength(500);
        builder.Property(b => b.City).HasMaxLength(100);
        builder.Property(b => b.State).HasMaxLength(50);
        builder.Property(b => b.ZipCode).HasMaxLength(10);
        builder.Property(b => b.Phone).HasMaxLength(20);
        builder.Property(b => b.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.UpdatedAt);

        // Indexes for better query performance
        builder.HasIndex(b => b.Code).IsUnique();
        builder.HasIndex(b => b.IsActive);
    }
}