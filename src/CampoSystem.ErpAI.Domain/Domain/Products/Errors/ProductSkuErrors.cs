using CampoSystem.ErpAI.Domain.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.Result;

namespace CampoSystem.ErpAI.Domain.Domain.Products.Errors;

public static class ProductSkuErrors
{
    public static readonly Error Required = new(
        code: "Product.Sku.Required",
        field: nameof(ProductSku),
        message: "Sku do produto requerido."
    );

    public static readonly Error InvalidFormat = new(
        code: "Product.Sku.Invalid_Format",
        field: nameof(ProductSku),
        message: "Formato do sku do produto inválido."
    );


}