using AsyncProductApi.Contract.Product;
using AsyncProductApi.Core.Product.Handlers;
using AsyncProductApi.Domain.RequestInfo;
using MediatR;
using MinimalApi.Endpoint;

namespace AsyncProductApi.Endpoints.CreateProducts;

internal sealed class GetProductsByRequestId : IEndpoint<IResult, Guid, CancellationToken>
{
    private readonly IMediator _mediator;

    public GetProductsByRequestId(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/productsByRequestId/{requestId}",
                async (
                    Guid requestId,
                    CancellationToken cancellationToken) => await HandleAsync(requestId, cancellationToken))
            .Produces<IEnumerable<ProductDto>>()
            .Produces(StatusCodes.Status404NotFound);
    }

    public async Task<IResult> HandleAsync(
        Guid requestId, 
        CancellationToken cancellationToken)
    {
        var getProductsByRequestInfoIdResult = await _mediator.Send(
            new GetProductsByRequestInfoIdRequest(new RequestInfoId(requestId)), 
            cancellationToken);

        return !getProductsByRequestInfoIdResult.ProductDtos.Any() 
            ? Results.NotFound() 
            : Results.Ok(getProductsByRequestInfoIdResult.ProductDtos);
    }
}