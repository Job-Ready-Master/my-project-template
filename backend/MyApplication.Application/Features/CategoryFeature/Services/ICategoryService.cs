using MyApplication.Application.Features.CategoryFeature.DTO;

namespace MyApplication.Application.Features.CategoryFeature.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllAsync(CancellationToken token);
    Task<CategoryResponse?> GetByIdAsync(int id, CancellationToken token);
    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request, CancellationToken token);
    Task<bool> DeleteAsync(int id, CancellationToken token);
}