using AsyncProductApi.Contract.RequestInfo;
using AsyncProductApi.Core.RequestInfo.Handlers;
using MediatR;
using MinimalApi.Endpoint;

namespace AsyncProductApi.Endpoints.CreateProducts;

internal sealed class GetProductsStatus : IEndpoint<IResult, Guid, CancellationToken>
{
    private readonly IMediator _mediator;

    public GetProductsStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/productsStatus/{requestId}",
                async (
                    Guid requestId,
                    CancellationToken cancellationToken) => await HandleAsync(requestId, cancellationToken))
            .Produces<string>()
            .Produces(StatusCodes.Status302Found);
    }

    public async Task<IResult> HandleAsync(
        Guid requestId, 
        CancellationToken cancellationToken)
    {
        var getRequestInfoByIdResult = await _mediator.Send(
            new GetRequestInfoByIdRequest(new RequestInfoId(requestId)),
            cancellationToken);

        if (getRequestInfoByIdResult.RequestInfoDto.IsCompleted)
        {
            var resourceUrl = $"api/v1/productsByRequestId/{requestId}";

            return Results.Redirect("https://localhost:7246/" + resourceUrl);
        }

        return Results.Ok(getRequestInfoByIdResult.RequestInfoDto.RequestStatus.ToString());
    }
}