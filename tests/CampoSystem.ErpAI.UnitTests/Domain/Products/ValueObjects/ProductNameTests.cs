using CampoSystem.ErpAI.Domain.Domain.Products.Errors;
using CampoSystem.ErpAI.Domain.Domain.Products.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests;

public class ProductNameTests
{
    [Fact]
    public void Should_Return_Success_When_Name_Is_Valid()
    {
        //Act
        var result = ProductName.Create("Notebook");

        //Assert
        Assert.True(result.IsSuccess);

        Assert.Equal("Notebook", result.Value.Name);
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Should_Return_Faliure_When_Name_Is_Required(string? name)
    {
        // Arrange
        var error = ProductNameErrors.Required;
        // Act
        var result = ProductName.Create(name);
        //Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors.First(e => e.Code == error.Code && e.Field == error.Field));
    }

    [Fact]
    public void Should_Return_Faliure_When_Name_Minlength_Is_Smaller_3()
    {
        // Arrange
        var error = ProductNameErrors.MinLength;
        // Act
        var result = ProductName.Create("AB");
        //Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors.First(e => e.Code == error.Code && e.Field == error.Field));
    }

    [Fact]
    public void Should_Return_Faliure_When_Name_Maxlength_Is_Exceeded_30()
    {
        // Arrange
        var error = ProductNameErrors.MaxLength;
        // Act
        var result = ProductName.Create("momomomomomomomomomo momomomomomomomomomo momomomomomomomomomo");
        //Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors.First(e => e.Code == error.Code && e.Field == error.Field  ));
    }


}
