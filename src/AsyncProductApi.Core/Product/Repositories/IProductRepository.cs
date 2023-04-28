using AsyncProductApi.Domain.RequestInfo;

namespace AsyncProductApi.Core.Product.Repositories;

public interface IProductRepository
{
    Task AddProductsAsync(IEnumerable<Domain.Product.Product> products, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Product.Product>> GetProductsByIdAsync(RequestInfoId id);
}