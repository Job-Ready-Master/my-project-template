using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApplication.Domain;

namespace MyApplication.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasIndex(x => x.Slug)
            .IsUnique();
    }
}