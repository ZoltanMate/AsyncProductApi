using AsyncProductApi.Contract.RequestInfo;
using AsyncProductApi.Core.RequestInfo.Extensions;
using AsyncProductApi.Core.RequestInfo.Repositories;
using MediatR;

namespace AsyncProductApi.Core.RequestInfo.Handlers;

internal sealed class GetRequestInfoByIdHandler
    : IRequestHandler<GetRequestInfoByIdRequest, GetRequestInfoByIdResult>
{
    private readonly IRequestInfoRepository _requestInfoRepository;

    public GetRequestInfoByIdHandler(IRequestInfoRepository requestInfoRepository)
    {
        _requestInfoRepository = requestInfoRepository;
    }

    public async Task<GetRequestInfoByIdResult> Handle(
        GetRequestInfoByIdRequest request,
        CancellationToken cancellationToken)
    {
        var requestInfo = await _requestInfoRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        return new GetRequestInfoByIdResult(new RequestInfoDto(requestInfo.RequestStatus.ToContract()));
    }
}

public sealed record GetRequestInfoByIdRequest(RequestInfoId Id)
    : IRequest<GetRequestInfoByIdResult>;

public sealed record GetRequestInfoByIdResult(RequestInfoDto RequestInfoDto);