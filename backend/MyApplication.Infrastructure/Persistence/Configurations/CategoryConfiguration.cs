using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApplication.Domain;
using MyApplication.Infrastructure.Persistence.Configurations.Common;

namespace MyApplication.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfiguration : AuditableEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

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