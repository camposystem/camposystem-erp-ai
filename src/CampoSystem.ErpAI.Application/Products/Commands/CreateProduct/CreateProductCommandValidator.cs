using FluentValidation;

namespace CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{

    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Sku)
            .NotEmpty();
                                
    }

}
