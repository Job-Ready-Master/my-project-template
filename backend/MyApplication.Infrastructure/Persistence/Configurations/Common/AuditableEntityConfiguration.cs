using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApplication.Domain.Common;

namespace MyApplication.Infrastructure.Persistence.Configurations.Common;

public abstract class AuditableEntityConfiguration<TEntity>
    : BaseEntityConfiguration<TEntity>
    where TEntity : AuditableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.ModifiedAt)
            .IsRequired(false);
    }
}