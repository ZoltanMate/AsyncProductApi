using StronglyTypedIds;

namespace AsyncProductApi.Domain.Product;

[StronglyTypedId(converters: StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct ProductId
{
}