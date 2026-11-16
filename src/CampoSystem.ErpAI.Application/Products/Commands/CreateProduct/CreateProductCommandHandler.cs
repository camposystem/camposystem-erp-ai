using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Application.Products.Response;
using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.Result;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;
using FluentValidation;

namespace CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<CreateProductCommand> _validator;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IValidator<CreateProductCommand> validator)
    {
        _productRepository = productRepository;
        _validator = validator;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(error => new Error(
                    code: error.ErrorCode,
                    field: error.PropertyName,
                    message: error.ErrorMessage))
                .ToList();

            return Result<CreateProductResponse>.Failure(errors);
        }

        var name = ProductName.Create(command.Name);
        var sku = ProductSku.Create(command.Sku);
        var price = command.Price.HasValue ? Money.From(command.Price.Value) : null;

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
