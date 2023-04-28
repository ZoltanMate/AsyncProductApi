namespace AsyncProductApi.Contract.Product;

public sealed record CreateProductsRequest(IEnumerable<ProductDto> Products);