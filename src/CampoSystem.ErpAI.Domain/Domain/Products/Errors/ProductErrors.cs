using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.Result;

namespace CampoSystem.ErpAI.Domain.Products.Errors;

public static class ProductErrors
{


    public static readonly Error InvalidPrice = new(
        code: "Product.InvalidPrice",
        field: nameof(Product),
        message: "O preço do produto deve ser maior que zero."
    );

    public static readonly Error DescriptionTooLong = new(
        code: "Product.DescriptionTooLong",
        field: nameof(Product),
        message: "A descrição do produto não pode exceder 1000 caracteres."
    );

}