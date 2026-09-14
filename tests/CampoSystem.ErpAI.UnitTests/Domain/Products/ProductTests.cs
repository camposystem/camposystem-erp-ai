using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;
using System.Threading.Channels;

namespace CampoSystem.ErpAI.UnitTests.Domain.Products;

public class ProductTests
{
    [Theory]
    [InlineData("Playstation 5", "GAME_1234", 500.0)]
    public void Given_Product_With_Name_Sku_And_Price_When_Created_Then_Product_Should_Start_Inactive(
        string name, string sku, decimal price)
    {
        //arrange
        var productName = ProductName.Create(name);
        var productSku = ProductSku.Create(sku);
        var productPrice = Money.From(price);

        var product = new Product(productName.Value, productSku.Value, productPrice);


        //act
        var isActive = product.IsActive;

        //assert

        Assert.False(isActive);
    }

    [Theory]
    [InlineData("Playstation 5", "GAME_1234")]
    [InlineData("Playstation 6", "GAME_12345")]
    public void Given_Name_SKU_Valid_And_Price_Null_When_Product_Created_Is_Inactive(
        string name, string sku)
    {
        //arrange
        var productName = ProductName.Create(name);
        var productSku = ProductSku.Create(sku);
        Money? productPrice = null;

        var product = new Product(productName.Value, productSku.Value, productPrice);


        //act
        var isActive = product.IsActive;

        //assert

        Assert.False(isActive);
    }

    [Fact]
    public void Given_Inactive_Product_When_Price_Is_Informed_Then_Product_Should_Remaine_Inactive()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");

        Money? productPrice = null;

        var product = new Product(
            productName.Value,
            productSku.Value,
            productPrice);

        var price = Money.From(500);

        // Act
        product.ChangePrice(price);

        // Assert
        Assert.Equal(price, product.Price);
        Assert.False(product.IsActive);
    }

    [Fact]
    public void Given_Inactive_Product_When_Name_Sku_Is_Informed_And_Activate_Then_Product_Should_Become_Inactive()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");

        Money? productPrice = null;

        var product = new Product(
            productName.Value,
            productSku.Value,
            productPrice);

        //Act
        product.Activate();

        // Assert
        Assert.False(product.IsActive);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Given_Product_When_Invalid_Price_Is_Informed_Then_Should_Return_Failure(
        decimal value)
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");
        var product = new Product(
            productName.Value,
            productSku.Value,
            null);

        var price = Money.From(value);

        // Act
        var result = product.ChangePrice(price);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Null(product.Price);

    }

    [Fact]
    public void Given_Product_When_Valid_Price_Is_Informed_Then_Should_Return_Success()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");

        var product = new Product(
            productName.Value,
            productSku.Value,
            null);

        var price = Money.From(500);

        // Act
        var result = product.ChangePrice(price);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(price, product.Price);
        Assert.Equal(product, result.Value);
    }

    [Fact]
    public void Given_Inactive_Product_When_Price_Name_Sku_Is_Informed_And_Activate_Then_Product_Should_Become_Active()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");

        Money? productPrice = null;

        var product = new Product(
            productName.Value,
            productSku.Value,
            productPrice);

        var price = Money.From(500);

        // Act
        product.ChangePrice(price);
        product.Activate();

        // Assert
        Assert.Equal(price, product.Price);
        Assert.True(product.IsActive);
    }


    [Fact]
    public void Given_Product_When_ChangeName_Is_Called_Then_Name_Should_Be_Updated()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");
        var price = Money.From(500);

        var product = new Product(
            productName.Value,
            productSku.Value,
            price);

        var newName = ProductName.Create("Playstation 6");

        // Act
        product.ChangeName(newName.Value);

        // Assert
        Assert.Equal(newName.Value, product.Name);
    }


    [Fact]
    public void Given_Product_When_ChangeSku_Is_Called_Then_Sku_Should_Be_Updated()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");
        var price = Money.From(500);

        var product = new Product(
            productName.Value,
            productSku.Value,
            price);

        var newSku = ProductSku.Create("GAME_12345");

        // Act
        product.ChangeSku(newSku.Value);

        // Assert
        Assert.Equal(newSku.Value, product.Sku);
    }

    [Fact]
    public void Given_Product_When_ChangeDescription_Is_Called_Then_Description_Should_Be_Updated()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");
        var price = Money.From(500);

        var product = new Product(
            productName.Value,
            productSku.Value,
            price);

        var newDescription = "Updated description";

        // Act
        product.ChangeDescription(newDescription);

        // Assert
        Assert.Equal(newDescription, product.Description);
    }
    [Fact]
    public void Given_Active_Product_When_Deactivate_Then_Product_Should_Be_Inactive()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");
        var productPrice = Money.From(500);

        var product = new Product(
            productName.Value,
            productSku.Value,
            productPrice);

        // Act
        product.Deactivate();

        // Assert
        Assert.False(product.IsActive);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Given_Product_With_Invalid_Price_When_Activate_Then_Should_Remain_Inactive(
    decimal value)
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");
        var price = Money.From(value);

        var product = new Product(
            productName.Value,
            productSku.Value,
            price);

        // Act
        product.Activate();

        // Assert
        Assert.False(product.IsActive);
    }
    [Fact]
    public void Given_Product_When_Description_Exceeds_Max_Length_Then_Should_Return_Failure()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");

        var product = new Product(
            productName.Value,
            productSku.Value,
            Money.From(500));

        var description = new string('A', 1001);

        // Act
        var result = product.ChangeDescription(description);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(string.Empty, product.Description);
    }
    [Fact]
    public void Given_Product_When_Description_Has_Max_Length_Then_Should_Return_Success()
    {
        // Arrange
        var productName = ProductName.Create("Playstation 5");
        var productSku = ProductSku.Create("GAME_1234");

        var product = new Product(
            productName.Value,
            productSku.Value,
            Money.From(500));

        var description = new string('A', 1000);

        // Act
        var result = product.ChangeDescription(description);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(description, product.Description);
    }
}
