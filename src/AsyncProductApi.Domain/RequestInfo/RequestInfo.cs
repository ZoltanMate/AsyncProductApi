namespace AsyncProductApi.Domain.RequestInfo;

public class RequestInfo
{
    public RequestInfoId Id { get; } = RequestInfoId.New();

    public RequestStatus RequestStatus { get; set; }
}