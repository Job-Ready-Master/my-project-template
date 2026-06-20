namespace MyApplication.Application.Features.CategoryFeature.DTO;

public sealed class CategoryResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}