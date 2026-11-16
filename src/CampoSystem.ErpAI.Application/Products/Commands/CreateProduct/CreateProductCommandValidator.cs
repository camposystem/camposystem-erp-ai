using FluentValidation;

namespace CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{

    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("Product.Name.Required")
            .WithMessage("O nome do produto é obrigatório.");

        RuleFor(x => x.Sku)
            .NotEmpty()
            .WithErrorCode("Product.Sku.Required")
            .WithMessage("O SKU do produto é obrigatório.");

    }

}
