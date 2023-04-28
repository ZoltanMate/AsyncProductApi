using AsyncProductApi.Core.Product.Repositories;
using AsyncProductApi.Domain.Product;
using AsyncProductApi.Domain.RequestInfo;
using AsyncProductApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AsyncProductApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public ProductRepository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task AddProductsAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await dbContext.Products.AddRangeAsync(products, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByIdAsync(RequestInfoId id)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return await dbContext.Products
            .Where(x => x.RequestInfoId == id)
            .AsNoTracking()
            .ToListAsync();
    }
}