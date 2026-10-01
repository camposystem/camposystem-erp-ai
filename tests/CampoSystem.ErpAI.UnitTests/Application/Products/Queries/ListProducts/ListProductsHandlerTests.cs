using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;
using Moq;

namespace CampoSystem.ErpAI.Application.Products.Queries.ListProducts;

public sealed class ListProductsHandlerTests
{
    [Fact]
    public async Task Given_Valid_ListProductsQuery_When_Handler_Executes_Then_Returns_ListProductsResponse()
    {
        // Arrange

        var name = ProductName.Create("Test Product");
        var sku = ProductSku.Create("TEST-001");
        var price = Money.From(10.99m);
        var description = "Test Description";
        var product = new Product(name.Value, sku.Value, price, description);

        var name2 = ProductName.Create("Test Product 2");
        var sku2 = ProductSku.Create("TEST-002");
        var price2 = Money.From(20.99m);
        var description2 = "Test Description 2";
        var product2 = new Product(name2.Value, sku2.Value, price2, description2);

        var ilistProducts = new List<Product> { product, product2 };

        var productRepositoryMock = new Mock<IProductRepository>();

        var listProductsHandler = new ListProductsHandler(productRepositoryMock.Object);

        productRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(ilistProducts);

        // Act
        var result = await listProductsHandler.Handle(new ListProductsQuery());

        // Assert
        productRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);

        Assert.Equal(product.Id, result[0].Id);
        Assert.Equal(product2.Id, result[1].Id);

        Assert.Equal("Test Product", result[0].Name);
        Assert.Equal("TEST-001", result[0].Sku);
        Assert.Equal(10.99m, result[0].Price);
        Assert.Equal("Test Description", result[0].Description);

        Assert.Equal("Test Product 2", result[1].Name);
        Assert.Equal("TEST-002", result[1].Sku);
        Assert.Equal(20.99m, result[1].Price);
        Assert.Equal("Test Description 2", result[1].Description);
    }


    [Fact]

    public async Task Given_ListProductsQuery_When_Handler_Executes_Then_Returns_Empty_ListProductsResponse()
    {
        // Arrange

        var productRepositoryMock = new Mock<IProductRepository>();

        var listProductsHandler = new ListProductsHandler(productRepositoryMock.Object);

        productRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Product>());

        // Act
        var result = await listProductsHandler.Handle(new ListProductsQuery());

        // Assert
        productRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);

        Assert.Empty(result);
    }


}