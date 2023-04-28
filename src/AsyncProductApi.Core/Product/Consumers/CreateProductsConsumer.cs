using AsyncProductApi.Contract.Product;
using AsyncProductApi.Contract.RequestInfo;
using AsyncProductApi.Core.Product.Repositories;
using AsyncProductApi.Core.RequestInfo.Handlers;
using MassTransit;
using MediatR;

namespace AsyncProductApi.Core.Product.Consumers;

public sealed class CreateProductsConsumer
    : IConsumer<CreateProductsMessage>
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRepository;

    public CreateProductsConsumer(
        IMediator mediator,
        IProductRepository productRepository)
    {
        _mediator = mediator;
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<CreateProductsMessage> context)
    {
        var message = context.Message;
        var products = message.Products.Select(productDto => MapProduct(productDto, message.Id)).ToList();

        var requestInfoStatus = RequestStatus.Completed;

        try
        {
            await _productRepository.AddProductsAsync(products, CancellationToken.None);
        }
        catch (Exception)
        {
            requestInfoStatus = RequestStatus.Failed;
        }

        await _mediator.Send(new SetRequestInfoStatusRequest(message.Id, requestInfoStatus));
    }

    private static Domain.Product.Product MapProduct(
        ProductDto productDto,
        RequestInfoId requestInfoId)
    {
        return new Domain.Product.Product
        {
            Name = productDto.Name,
            RequestInfoId = requestInfoId
        };
    }
}

public sealed record CreateProductsMessage(
    IEnumerable<ProductDto> Products, 
    RequestInfoId Id);