namespace AsyncProductApi.Domain.RequestInfo;

internal interface IHasRequestInfo
{
    RequestInfoId RequestInfoId { get; set; }
}