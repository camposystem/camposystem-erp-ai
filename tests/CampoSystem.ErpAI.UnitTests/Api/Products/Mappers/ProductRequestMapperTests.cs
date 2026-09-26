using CampoSystem.ErpAI.Api.Products.Mappers;
using CampoSystem.ErpAI.Api.Products.Requests;

namespace CampoSystem.ErpAI.UnitTests.Api.Products.Mappers;

public class ProductRequestMapperTests
{
    [Fact]
    public void Given_A_CreateProductRequest_The_Mapper_Produces_A_CreateProductCommand_With_The_Same_Values()
    {
        // Arrange
        var request = new CreateProductRequest(
            Name: "Test Product",
            Sku: "TP001",
            Price: 99.99m,
            Description: "This is a test product.");
        // Act
        var command = ProductRequestMapper.ToCreateProductCommand(request);
        // Assert
        Assert.Equal(request.Name, command.Name);
        Assert.Equal(request.Sku, command.Sku);
        Assert.Equal(request.Price, command.Price);
        Assert.Equal(request.Description, command.Description);
    }
}
