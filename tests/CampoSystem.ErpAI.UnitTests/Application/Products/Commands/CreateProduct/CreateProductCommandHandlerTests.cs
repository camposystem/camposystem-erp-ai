using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;
using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Domain.Products;
using Moq;

namespace CampoSystem.ErpAI.UnitTests.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandlerTests
{


    [Fact]
    public async Task Given_Valid_CreateProductCommand_When_Handler_Executes_Then_Returns_Product_Id()
    {
        // Arrange
        Product? capturedProduct = null;

        var command = new CreateProductCommand("Test Product", "TEST-001", 10.99m);

        var productRepositoryMock = new Mock<IProductRepository>();

        productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(product => capturedProduct = product)
            .ReturnsAsync((Product product) =>
            {
                return product;
            });

        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value.Id);

        productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);

        Assert.Equal("Test Product", capturedProduct!.Name.Name);
        Assert.Equal("TEST-001", capturedProduct.Sku.Sku);
        Assert.Equal(10.99m, capturedProduct.Price!.Amount);
        Assert.Equal(string.Empty, capturedProduct.Description);
    }

    [Fact]
    public async Task Given_A_Product_Command_Invalid_Name_When_The_Handler_Executes_Should_Return_Failure_And_The_Repository_Should_Not_Be_Called()
    {
        // Arrange
        var command = new CreateProductCommand("", "TEST-001", 10.99m);
        var productRepositoryMock = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);
        // Act 
        var result = await handler.Handle(command);
        // Assert
        productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Never);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "Product.Name.Required");
    }


    [Fact]
    public async Task Given_A_Product_Command_Invalid_Sku_When_The_Handler_Executes_Should_Return_Failure_And_The_Repository_Should_Not_Be_Called()
    {
        // Arrange
        var command = new CreateProductCommand("Test Product", "", 10.99m);
        var productRepositoryMock = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(productRepositoryMock.Object);
        // Act 
        var result = await handler.Handle(command);
        // Assert
        productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Never);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "Product.Sku.Required");
    }
}
