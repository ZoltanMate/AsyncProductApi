using AsyncProductApi.Contract.RequestInfo;
using AsyncProductApi.Core.RequestInfo.Handlers;
using MediatR;
using MinimalApi.Endpoint;

namespace AsyncProductApi.Endpoints.Simulation;

internal sealed class SimulateProductsCompleted : IEndpoint<IResult, Guid, CancellationToken>
{
    private readonly IMediator _mediator;

    public SimulateProductsCompleted(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/simulateProductsCompleted/{requestId}",
                async (
                    Guid requestId,
                    CancellationToken cancellationToken) => await HandleAsync(requestId, cancellationToken))
            .Produces(StatusCodes.Status200OK);
    }

    public async Task<IResult> HandleAsync(
        Guid requestInfoId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new SetRequestInfoStatusRequest(new RequestInfoId(requestInfoId), RequestStatus.Completed),
            cancellationToken);

        return Results.Ok();
    }
}