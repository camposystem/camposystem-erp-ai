using CampoSystem.ErpAI.Domain.Products.Errors;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.Result;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.Domain.Products;

public sealed class Product : Entity
{

    public ProductName Name { get; private set; }
    public ProductSku Sku { get; private set; }
    public Money? Price { get; private set; }

    public string Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = false;

    public Product(ProductName name, ProductSku sku, Money? price)
    {
        Name = name;
        Sku = sku;
        Price = price;
            }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = Name is not null && Sku is not null && Price is not null && Price.Amount > 0;

    public Result<Product> ChangePrice(Money newPrice)
    {
        if(newPrice.Amount <= 0)
        {
            return Result<Product>.Failure([ProductErrors.InvalidPrice]);
        }

        Price = newPrice;
        return Result<Product>.Success(this);
    }

    public void ChangeName(ProductName value)
    {
        Name = value;
    }

    public void ChangeSku(ProductSku value)
    {
        Sku = value ;
    }

    public Result<Product> ChangeDescription(string value)
    {
        if (value.Length > 1000)
        {
            return Result<Product>.Failure([ProductErrors.DescriptionTooLong]);
        }

        Description = value;
        return Result<Product>.Success(this);
    }

}
