namespace AsyncProductApi.Core.RequestInfo.Extensions;

internal static class RequestStatusExtensions
{
    public static Contract.RequestInfo.RequestStatus ToContract(this Domain.RequestInfo.RequestStatus requestStatus)
    {
        return requestStatus switch
        {
            Domain.RequestInfo.RequestStatus.Completed => Contract.RequestInfo.RequestStatus.Completed,
            Domain.RequestInfo.RequestStatus.Pending => Contract.RequestInfo.RequestStatus.Pending,
            Domain.RequestInfo.RequestStatus.Failed => Contract.RequestInfo.RequestStatus.Failed,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static Domain.RequestInfo.RequestStatus ToDomain(this Contract.RequestInfo.RequestStatus requestStatus)
    {
        return requestStatus switch
        {
            Contract.RequestInfo.RequestStatus.Completed => Domain.RequestInfo.RequestStatus.Completed,
            Contract.RequestInfo.RequestStatus.Pending => Domain.RequestInfo.RequestStatus.Pending,
            Contract.RequestInfo.RequestStatus.Failed => Domain.RequestInfo.RequestStatus.Failed,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}