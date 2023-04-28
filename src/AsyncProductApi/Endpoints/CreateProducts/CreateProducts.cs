using AsyncProductApi.Contract.Product;
using AsyncProductApi.Contract.RequestInfo;
using AsyncProductApi.Core.Product.Consumers;
using AsyncProductApi.Core.RequestInfo.Handlers;
using MassTransit;
using MediatR;
using MinimalApi.Endpoint;

namespace AsyncProductApi.Endpoints.CreateProducts;

internal sealed class CreateProducts : IEndpoint<IResult, CreateProductsRequest, CancellationToken>
{
    private readonly IMediator _mediator;
    private readonly IBus _bus;

    public CreateProducts(
        IMediator mediator,
        IBus bus)
    {
        _mediator = mediator;
        _bus = bus;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/products",
                async (
                    CreateProductsRequest request,
                    CancellationToken cancellationToken) => await HandleAsync(request, cancellationToken))
            .Produces(StatusCodes.Status202Accepted);
    }

    public async Task<IResult> HandleAsync(
        CreateProductsRequest request,
        CancellationToken cancellationToken)
    {
        var createRequestInfoResult = await _mediator.Send(new CreateRequestInfoRequest(), cancellationToken);

        // Start Long Running Process....
        await _bus.Publish(
            new CreateProductsMessage(request.Products, new RequestInfoId(createRequestInfoResult.Id.Value)),
            cancellationToken);

        return Results.Accepted($"api/v1/productsStatus/{createRequestInfoResult.Id}");
    }
}