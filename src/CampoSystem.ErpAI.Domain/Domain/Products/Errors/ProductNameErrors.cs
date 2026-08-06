using CampoSystem.ErpAI.Domain.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Result;

namespace CampoSystem.ErpAI.Domain.Domain.Products.Errors;

public static class ProductNameErrors
{
    public static readonly Error Required    = new(
        code: "Product.Name.Required",
        field: nameof(ProductName),
        message: "Nome do produto requerido"
    );

    public static readonly Error MinLength = new(
        code: "Product.Name.MinLength",
        field: nameof(ProductName),
        message: "Nome do produto deve ter pelo menos 3 caracteres."
    );

    public static readonly Error MaxLength = new(
        code: "Product.Name.MaxLength",
        field: nameof(ProductName),
        message: "Nome do produto deve ter até 30 caracteres."
    );
}