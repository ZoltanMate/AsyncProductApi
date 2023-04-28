using AsyncProductApi.Core.RequestInfo.Exceptions;
using AsyncProductApi.Core.RequestInfo.Repositories;
using AsyncProductApi.Domain.RequestInfo;
using AsyncProductApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AsyncProductApi.Infrastructure.Repositories;

public class RequestInfoRepository : IRequestInfoRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public RequestInfoRepository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<RequestInfoId> AddRequestInfoAsync(CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var requestInfo = new RequestInfo
        {
            RequestStatus = RequestStatus.Pending
        };

        await dbContext.RequestInfos.AddAsync(requestInfo, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return requestInfo.Id;
    }

    public async Task<RequestInfo> GetByIdAsync(RequestInfoId id, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var requestInfo = await GetById(id, dbContext, cancellationToken);

        return requestInfo;
    }

    public async Task SetStatus(RequestInfoId id, RequestStatus status, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var requestInfo = await GetById(id, dbContext, cancellationToken);

        requestInfo.RequestStatus = status;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task<RequestInfo> GetById(
        RequestInfoId id, 
        AppDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var requestInfo = await dbContext.RequestInfos.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (requestInfo == null)
        {
            throw new RequestInfoNotFoundException($"Request not found by {requestInfo} id");
        }

        return requestInfo;
    }
}