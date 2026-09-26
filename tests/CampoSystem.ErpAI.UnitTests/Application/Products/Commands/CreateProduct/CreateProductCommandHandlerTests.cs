using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;
using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;
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

    [Fact]
    public async Task Given_A_Valid_Command_The_Handler_Should_Create_The_Product_Call_The_Repository_And_Return_Success_With_The_Id()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "Test Product",
            Sku: "TESTSKU",
            Price: 10.99m,
            Description: "Test Description"
        );
        var name = ProductName.Create(command.Name);
        var sku = ProductSku.Create(command.Sku);
        var price = command.Price.HasValue ? Money.From(command.Price.Value) : null;
        var product = new Product(name.Value, sku.Value, price, command.Description);
        var mockRepository = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(mockRepository.Object);

        mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(product);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(product.Id, result.Value.Id);

        mockRepository.Verify(repo => repo.AddAsync(It.Is<Product>(p =>
            p.Name.Name == command.Name &&
            p.Sku.Sku == command.Sku &&
            p.Price!.Amount == command.Price &&
            p.Description == command.Description)), Times.Once);

    }

    [Fact]
    public async Task Given_An_Invalid_Product_Name_The_Handler_Should_Return_An_Error_And_Should_Not_Call_The_Repository()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "",
            Sku: "TESTSKU",
            Price: 10.99m,
            Description: "Test Description"
        );

        var mockRepository = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Field == nameof(ProductName));

        mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Never);
    }


    [Fact]
    public async Task Given_An_Invalid_Product_Sku_The_Handler_Should_Return_An_Error_And_Should_Not_Call_The_Repository()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "Test Product",
            Sku: "",
            Price: 10.99m,
            Description: "Test Description"
        );

        var mockRepository = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Field == nameof(ProductSku));

        mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task Given_A_Product_Without_Price_The_Handler_Should_Allow_Its_Creation()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "Test Product",
            Sku: "TESTSKU",
            Price: null,
            Description: "Test Description"
        );
        var name = ProductName.Create(command.Name);
        var sku = ProductSku.Create(command.Sku);
        var price = command.Price.HasValue ? Money.From(command.Price.Value) : null;
        var product = new Product(name.Value, sku.Value, price, command.Description);
        var mockRepository = new Mock<IProductRepository>();
        var handler = new CreateProductCommandHandler(mockRepository.Object);

        mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(product);

        // Act
        var result = await handler.Handle(command);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(product.Id, result.Value.Id);

        mockRepository.Verify(repo => repo.AddAsync(It.Is<Product>(p =>
            p.Name.Name == command.Name &&
            p.Sku.Sku == command.Sku &&
            p.Price == null &&
            p.Description == command.Description)), Times.Once);

    }
}
