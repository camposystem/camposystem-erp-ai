using CampoSystem.ErpAI.Domain.Domain.Products.Errors;
using CampoSystem.ErpAI.SharedKernel.Common.Result;

namespace CampoSystem.ErpAI.Domain.Domain.Products.ValueObjects;

public sealed record ProductName
{

    public string Name { get; private set; } = string.Empty;

    public ProductName(string name)
    {
        Name = name;
    }

    public static Result<ProductName> Create(string name)
    {

        if (string.IsNullOrWhiteSpace(name))
            return Result<ProductName>.Failure([ProductNameErrors.Required]);

        if (name.Length < 3)
            return Result<ProductName>.Failure([ProductNameErrors.MinLength]);

        if (name.Length > 30)
            return Result<ProductName>.Failure([ProductNameErrors.MaxLength]);

        return Result<ProductName>.Success(new ProductName(name));
    }
}
