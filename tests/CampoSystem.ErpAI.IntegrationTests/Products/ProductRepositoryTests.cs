using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.Infrastructure.Persistence;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CampoSystem.ErpAI.IntegrationTests.Products;

public class ProductRepositoryTests
{
    private readonly ProductDbContext _dbContext;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=camposystem;Username=postgres;Password=postgres123")
            .Options;

        _dbContext = new ProductDbContext(options);
    }

    [Fact]
    public async Task Should_Add_Product()
    {
        // Arrange
        var productName = new ProductName("Product Test");
        var productSku = new ProductSku("SKU123");
        var productPrice =  Money.From(10.99m);

        var product = new Product(productName, productSku, productPrice);

        var repository = new ProductRepository(_dbContext);

        // Act
        var result = await repository.AddAsync(product);

        var persistedProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == result.Id);
        // Assert
        Assert.NotNull(persistedProduct);
        Assert.Equal("Product Test", persistedProduct.Name.Name);
        Assert.Equal("SKU123", persistedProduct.Sku.Sku);
        Assert.Equal(10.99m, persistedProduct.Price!.Amount);
    }
}
