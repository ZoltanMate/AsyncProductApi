namespace AsyncProductApi.Contract.RequestInfo;

public sealed record RequestInfoDto(RequestStatus RequestStatus)
{
    public bool IsCompleted => RequestStatus == RequestStatus.Completed;
}