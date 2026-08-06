using CampoSystem.ErpAI.Domain.Domain.Products.Errors;
using CampoSystem.ErpAI.SharedKernel.Result;
using System.Text.RegularExpressions;

namespace CampoSystem.ErpAI.Domain.Domain.Products.ValueObjects;

public sealed record ProductSku
{
    public string Sku { get; } = string.Empty;

    public ProductSku(string sku)
    {
        Sku = sku;
    }

    public static Result<ProductSku> Create(string? sku)
    {

        if (string.IsNullOrWhiteSpace(sku))
        {
            return Result<ProductSku>.Failure([ProductSkuErrors.Required]);
        }

        sku = sku?.Trim().ToUpperInvariant();

        if (!Regex.IsMatch(sku, @"^[A-Z0-9\-_]+$"))
        {
            return Result<ProductSku>.Failure([ProductSkuErrors.InvalidFormat]);
        }

        return Result<ProductSku>.Success(new ProductSku(sku));
    }
}
