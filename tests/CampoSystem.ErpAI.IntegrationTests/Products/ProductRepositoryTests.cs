using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;
using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.Infrastructure.Persistence;
using CampoSystem.ErpAI.IntegrationTests.Infrastructure;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace CampoSystem.ErpAI.IntegrationTests.Products;

public class ProductRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly ProductDbContext _dbContext;

    private readonly DatabaseFixture _databaseFixture;

    public ProductRepositoryTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;

        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseNpgsql(_databaseFixture.ConnectionString)
            .Options;

        _dbContext = new ProductDbContext(options);
    }

    [Fact]
    public async Task Should_Add_Product()
    {
        // Arrange

        await _databaseFixture.ResetDatabaseAsync();

        var productName = new ProductName("Product Test");
        var productSku = new ProductSku("SKU123");
        var productPrice = Money.From(10.99m);

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



    [Fact]
    public async Task Given_A_Valid_Product_Command_The_Handler_Should_Persist_The_Product()
    {

        // Arrange
        await _databaseFixture.ResetDatabaseAsync();

        var command = new CreateProductCommand(
            Name: "Test Product",
            Sku: "TESTSKU",
            Price: 10.99m,
            Description: "Test Description"
        );

        var name = ProductName.Create(command.Name);
        var sku = ProductSku.Create(command.Sku);
        var price = command.Price.HasValue ? Money.From(command.Price.Value) : null;

        var repository = new ProductRepository(_dbContext);

        var handler = new CreateProductCommandHandler(repository);

        // Act
        var result = await handler.Handle(command);

        var persistedProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == result.Value.Id);

        // Assert
        Assert.True(result.IsSuccess);

        Assert.NotNull(persistedProduct);

        Assert.Equal(command.Name, persistedProduct.Name.Name);
        Assert.Equal(command.Sku, persistedProduct.Sku.Sku);
        Assert.Equal(command.Price, persistedProduct.Price!.Amount);
    }

    [Fact]
    public async Task Should_Get_All_Products()
    {
        // Arrange
        await _databaseFixture.ResetDatabaseAsync();

        var repository = new ProductRepository(_dbContext);

        var product1 = new Product(
            ProductName.Create("Product 1").Value,
            ProductSku.Create("SKU-001").Value,
            Money.From(10.99m),
            "Description 1");

        var product2 = new Product(
            ProductName.Create("Product 2").Value,
            ProductSku.Create("SKU-002").Value,
            Money.From(20.99m),
            "Description 2");

        _dbContext.Products.AddRange(product1, product2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Id == product1.Id);
        Assert.Contains(result, p => p.Id == product2.Id);
    }


}
