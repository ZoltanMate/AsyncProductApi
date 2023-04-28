namespace AsyncProductApi.Contract.RequestInfo;

public sealed record RequestInfoId(Guid Value)
{
    public static implicit operator Domain.RequestInfo.RequestInfoId(RequestInfoId id) => new(id.Value);
}