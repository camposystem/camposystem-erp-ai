using CampoSystem.ErpAI.SharedKernel.Result;

namespace CampoSystem.ErpAI.UnitTests.SharedKernel.Results;

public class ResultTests
{
    /*
     * REQ-01 — Result.Success() deve ser sucesso e possuir zero erros.
     * Result.Failure(error) deve ser falha e preservar exatamente o erro recebido.
     */
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

}
