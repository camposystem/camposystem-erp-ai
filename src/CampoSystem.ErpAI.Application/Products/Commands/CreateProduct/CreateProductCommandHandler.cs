using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Application.Response;
using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.Result;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand command)
    {
        var name = ProductName.Create(command.Name);
        var sku = ProductSku.Create(command.Sku);
        var price = Money.From(command.Price);

        if (name.IsFailure)
        {
            return Result<CreateProductResponse>.Failure(name.Errors);
        }

        if (sku.IsFailure)
        {
            return Result<CreateProductResponse>.Failure(sku.Errors);
        }

        var product = new Product(name.Value, sku.Value, price, command.Description);

        var addedProduct = await _productRepository.AddAsync(product);

        return  Result<CreateProductResponse>.Success(new CreateProductResponse(addedProduct.Id));
    }
}
