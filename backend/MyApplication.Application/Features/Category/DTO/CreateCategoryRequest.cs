namespace MyApplication.Application.Features.Category.DTO;

public sealed class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;
}
