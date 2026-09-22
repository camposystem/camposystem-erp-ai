using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;
using Moq;
using System.Xml.Linq;

namespace CampoSystem.ErpAI.UnitTests.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidatorTests
{
    [Fact]
    public void Given_A_Product_Command_With_Empty_Name_When_Validating_Should_Indicate_That_Name_Is_Required()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "",
            Sku: "SKU123",
            Price: 100.0m
        );

        var validator = new CreateProductCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateProductCommand.Name));
    }

    [Fact]
    public void Given_A_Product_Command_With_Empty_Sku_When_Validating_Should_Indicate_That_Sku_Is_Required ()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "New Product",
            Sku: "",
            Price: 100.0m
        );

        var validator = new CreateProductCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateProductCommand.Sku));
    }



    [Fact]
    public void Given_A_Product_Command_With_Null_Price_When_Validating_Should_Indicate_That_Price_Is_Valid()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "New Product",
            Sku: "SKU123",
            Price: null
        );

        var validator = new CreateProductCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
        Assert.DoesNotContain(result.Errors, e => e.PropertyName == nameof(CreateProductCommand.Price));
    }












}
