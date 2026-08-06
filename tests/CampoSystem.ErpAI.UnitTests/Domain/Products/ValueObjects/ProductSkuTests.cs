using CampoSystem.ErpAI.Domain.Domain.Products.Errors;
using CampoSystem.ErpAI.Domain.Domain.Products.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.Domain.Products.ValueObjects;

public class ProductSkuTests
{


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Should_Return_Failure_When_Sku_Is_Required(string? sku)
    {
        //Arrange
        var error = ProductSkuErrors.Required;
        //Act
        var result = ProductSku.Create(sku);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == error.Code && e.Field == error.Field);
    }

    [Theory]
    [InlineData("sku-with-lowercase")]
    [InlineData("SKU_with_lowercase")]
    [InlineData("sku123")]
    [InlineData("SKU123lowercase")]
    [InlineData("sku")]

    public void Should_Normalize_Sku_To_Uppercase(string sku)
    {
        //Arrange
        var error = ProductSkuErrors.InvalidFormat;
        //Act
        var result = ProductSku.Create(sku);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(sku.ToUpper(), result.Value.Sku);
    }

    [Theory]
    [InlineData("SKU-123")]
    [InlineData("SKU_WITH_LOWERCASE")]
    [InlineData("SKU123")]
    [InlineData("SKU123LOWERCASE")]
    [InlineData("SKU")]
    public void Should_Return_Success_When_Sku_Is_Valid(string sku)
    {
        //Act
        var result = ProductSku.Create(sku);

        //Assert
        Assert.True(result.IsSuccess);

        Assert.Equal(sku, result.Value.Sku);
    }
}
