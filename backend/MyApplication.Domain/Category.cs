namespace MyApplication.Domain;

public class Category
{
    public int Id { get; set; }

    public string Name { get; private set; } = null!;

    public string Slug { get; private set; } = null!;

    protected Category() { }

    public Category(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }
}