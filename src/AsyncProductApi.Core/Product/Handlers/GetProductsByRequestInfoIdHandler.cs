using AsyncProductApi.Contract.Product;
using AsyncProductApi.Core.Product.Repositories;
using AsyncProductApi.Domain.RequestInfo;
using MediatR;

namespace AsyncProductApi.Core.Product.Handlers;

internal sealed class GetProductsByRequestInfoIdHandler
    : IRequestHandler<GetProductsByRequestInfoIdRequest, GetProductsByRequestInfoIdResult>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByRequestInfoIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductsByRequestInfoIdResult> Handle(
        GetProductsByRequestInfoIdRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsByIdAsync(request.Id);

        return new GetProductsByRequestInfoIdResult(products.Select(x => new ProductDto(x.Name)));
    }
}

public sealed record GetProductsByRequestInfoIdRequest(RequestInfoId Id)
    : IRequest<GetProductsByRequestInfoIdResult>;

public sealed record GetProductsByRequestInfoIdResult(IEnumerable<ProductDto> ProductDtos);