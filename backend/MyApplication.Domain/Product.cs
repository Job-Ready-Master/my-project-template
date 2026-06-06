using MyApplication.Domain.Common;

namespace MyApplication.Domain;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; } = null!;

    public decimal Price { get; private set; }

    public int CategoryId { get; private set; }

    public Category Category { get; private set; } = null!;

    protected Product() { }

    public Product(string name, decimal price, int categoryId)
    {
        Name = name;
        Price = price;
        CategoryId = categoryId;
    }
}