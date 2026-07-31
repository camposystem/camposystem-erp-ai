
using CampoSystem.ErpAI.SharedKernel.Result;

namespace CampoSystem.ErpAI.Domain.Entites.Products.ValueObjects;

public sealed record ProductName
{
    public string Name { get; private set; } = string.Empty;

    public ProductName(string name)
    {
        Name = name;
    }

    public static Result<ProductName> Create(string name)
    {
        var errorList =  new List<Error>();
        if (string.IsNullOrWhiteSpace(name))
            errorList.Add(ProductErrors.EmptyName);

        if (name.Length < 3)
            errorList.Add(ProductErrors.MinLength);

        if (name.Length > 30)
            errorList.Add(ProductErrors.MaxLength);

        if (errorList.Count > 0)
            return Result<ProductName>.Failure(errorList);


        return Result<ProductName>.Success(new ProductName(name));
    }
}

public static class ProductErrors
{
    public static readonly Error EmptyName = new(
        code: "Product.Name.Empty",
        field: nameof(ProductName),
        message: "Nome do produto não pode ser vazio."
    );

    public static readonly Error MinLength = new(
        code: "Product.Name.Empty",
        field: nameof(ProductName),
        message: "Nome do produto deve ter pelo menos 3 caracteres."
    );

    public static readonly Error MaxLength = new(
        code: "Product.Name.Empty",
        field: nameof(ProductName),
        message: "Nome do produto deve ter até 30 caracteres."
    );
}