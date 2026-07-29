using CampoSystem.ErpAI.Domain.Entites.Products.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests;

public class ProductNameTests
{
    [Fact]
    public void DeveRetornarFalhaQuandoNomeForVazio()
    {
        var result = ProductName.Create("");
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors.First(e => e.Message == "Nome do produto não pode ser vazio."));
    }

    [Fact]
    public void DeveRetornarFalhaQuandoNomeMenorQue3Caracteres()
    {
        var result = ProductName.Create("AB");
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors.First(e => e.Message == "Nome do produto deve ter pelo menos 3 caracteres."));
    }

    [Fact]
    public void DeveRetornarFalhaQuandoNomeMaiorQue30Caracteres()
    {
        var result = ProductName.Create("momomomomomomomomomo momomomomomomomomomo momomomomomomomomomo");
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors.First(e => e.Message == "Nome do produto deve ter até 30 caracteres."));
    }

    [Fact]
    public void DeveRetornaSucessoQuandoNomeValido()
    {
        var result = ProductName.Create("Notebook");
        Assert.True(result.IsSuccess);
        Assert.Equal("Notebook", result.Value.Name);
    }
}
