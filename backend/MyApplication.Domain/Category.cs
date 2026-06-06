using MyApplication.Domain.Common;

namespace MyApplication.Domain;

public sealed class Category : AuditableEntity
{
    public string Name { get; private set; } = null!;

    public string Slug { get; private set; } = null!;

    protected Category() { }

    public Category(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }
}