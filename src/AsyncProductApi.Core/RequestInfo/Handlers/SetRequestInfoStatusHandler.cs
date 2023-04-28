using AsyncProductApi.Contract.RequestInfo;
using AsyncProductApi.Core.RequestInfo.Extensions;
using AsyncProductApi.Core.RequestInfo.Repositories;
using MediatR;

namespace AsyncProductApi.Core.RequestInfo.Handlers;

internal sealed class SetRequestInfoStatusHandler
    : IRequestHandler<SetRequestInfoStatusRequest>
{
    private readonly IRequestInfoRepository _requestInfoRepository;

    public SetRequestInfoStatusHandler(IRequestInfoRepository requestInfoRepository)
    {
        _requestInfoRepository = requestInfoRepository;
    }

    public async Task Handle(SetRequestInfoStatusRequest request, CancellationToken cancellationToken)
    {
        await _requestInfoRepository.SetStatus(request.Id, request.RequestStatus.ToDomain(), cancellationToken);
    }
}

public sealed record SetRequestInfoStatusRequest(RequestInfoId Id, RequestStatus RequestStatus)
    : IRequest;