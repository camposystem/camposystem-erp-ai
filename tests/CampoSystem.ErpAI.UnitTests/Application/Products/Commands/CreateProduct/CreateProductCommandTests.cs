using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;

namespace CampoSystem.ErpAI.UnitTests.Application.Products.Commands.CreateProduct;

public class CreateProductCommandTests
{
    [Fact]
    public void Given_Product_Create_Command_without_Description_When_the_Command_is_created_Then_the_Description_must_be_Empty()
    {
        // Arrange
        var name = "Test Product";
        var sku = "TEST-001";
        var price = 10.99m;

        // Act
        var command = new CreateProductCommand(name, sku, price);

        // Assert
        Assert.Equal("", command.Description);
    }
}
