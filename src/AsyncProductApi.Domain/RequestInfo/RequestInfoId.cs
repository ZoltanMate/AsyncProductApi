using StronglyTypedIds;

namespace AsyncProductApi.Domain.RequestInfo;

[StronglyTypedId(converters: StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct RequestInfoId
{
}