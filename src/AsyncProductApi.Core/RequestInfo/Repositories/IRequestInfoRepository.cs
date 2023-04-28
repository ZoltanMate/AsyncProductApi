using AsyncProductApi.Domain.RequestInfo;

namespace AsyncProductApi.Core.RequestInfo.Repositories;

public interface IRequestInfoRepository
{
    Task<RequestInfoId> AddRequestInfoAsync(CancellationToken cancellationToken);
    Task<Domain.RequestInfo.RequestInfo> GetByIdAsync(RequestInfoId id, CancellationToken cancellationToken);
    Task SetStatus(RequestInfoId id, RequestStatus status, CancellationToken cancellationToken);
}