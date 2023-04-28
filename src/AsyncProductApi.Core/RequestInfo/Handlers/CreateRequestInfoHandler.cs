using AsyncProductApi.Core.RequestInfo.Repositories;
using AsyncProductApi.Domain.RequestInfo;
using MediatR;

namespace AsyncProductApi.Core.RequestInfo.Handlers;

internal sealed class CreateRequestInfoHandler
    : IRequestHandler<CreateRequestInfoRequest, CreateRequestInfoResult>
{
    private readonly IRequestInfoRepository _requestInfoRepository;

    public CreateRequestInfoHandler(IRequestInfoRepository requestInfoRepository)
    {
        _requestInfoRepository = requestInfoRepository;
    }

    public async Task<CreateRequestInfoResult> Handle(
        CreateRequestInfoRequest request,
        CancellationToken cancellationToken)
    {
        var requestInfoId = await _requestInfoRepository.AddRequestInfoAsync(cancellationToken);

        return new CreateRequestInfoResult
        {
            Id = requestInfoId
        };
    }
}

public sealed class CreateRequestInfoRequest
    : IRequest<CreateRequestInfoResult>
{
}

public sealed class CreateRequestInfoResult
{
    public RequestInfoId Id { get; set; }
}