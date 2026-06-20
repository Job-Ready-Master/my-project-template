using MyApplication.Application.Features.ProductFeature.DTO;

namespace MyApplication.Application.Features.ProductFeature.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<ProductResponse?> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
