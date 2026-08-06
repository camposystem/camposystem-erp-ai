using CampoSystem.ErpAI.Domain.Domain.Products.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Result;

namespace CampoSystem.ErpAI.UnitTests.SharedKernel.Results;

public class ResultTests
{
    [Fact]
    public void Success_Should_Return_Success_With_No_Errors()
    {
        // Arrange
        var productName = "Micro Ondas Consul";

        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Failure_Should_Return_Failure_With_Provided_Error()
    {
        // Arrange
        var error = new Error("PRODUCT.INVALID", "Produto inválido", "Product");

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.True(result.IsFailure);

        Assert.Single(result.Errors);

        Assert.Contains(
            result.Errors,
            e => e.Code == "PRODUCT.INVALID");
    }


    [Fact]
    public void Failure_Should_Throw_When_Accessing_Value()
    {
        // Arrange
        var errors = new List<Error>
        {
            new Error("PRODUCT.INVALID", "Produto inválido", "Product")
        };


        // Act
        var result = Result<ProductName>.Failure(errors);

        // Assert

        Assert.Throws<InvalidOperationException>(() =>
        {
            _ = result.Value;
        });

    }

    [Fact]
    public void Success_Should_Return_Provided_Value()
    {
        // Arrange
        var value = new ProductName("Televisão TCL");

        // Act
        var result = Result<ProductName>.Success(value);

        // Assert
        Assert.Same(value, result.Value);

    }

    [Fact]
    public void Success_Should_Return_Exception_Null_Value()
    {

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            _ = Result<ProductName>.Success(null!);
        });

    }

    [Fact]
    public void Failure_Should_Not_Change_When_Source_Error_Collection_Changes()
    {
        // Arrange
        var errors = new List<Error>
        {
            new Error("PRODUCT.INVALID", "Produto inválido", "Product")
        };

        // Act
        var result = Result<ProductName>.Failure(errors);

        errors.Add(
            new Error("PRODUCT.INVALID2", "Produto inválido 2", "Product")
        );

        // Assert
        Assert.Single(result.Errors);

    }


    [Fact]
    public void Failure_Should_Throw_When_Errors_Is_Null()
    {

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            _ = Result<ProductName>.Failure(null!);
        });

    }
}
