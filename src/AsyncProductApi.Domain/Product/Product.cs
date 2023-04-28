using AsyncProductApi.Domain.RequestInfo;

namespace AsyncProductApi.Domain.Product;

public class Product : IHasRequestInfo
{
    public ProductId Id { get; } = ProductId.New();

    public string Name { get; set; } = string.Empty;
    
    public RequestInfoId RequestInfoId { get; set; }
}