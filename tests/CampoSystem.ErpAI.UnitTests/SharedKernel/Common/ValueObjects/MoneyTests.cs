using CampoSystem.ErpAI.SharedKernel.Common.Exceptions;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.SharedKernel.Common.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Should_BeEqual_When_AmountsAreEqual()
    {
        // Arrange
        var money1 = Money.From(100m);
        var money2 = Money.From(100m);
        // Act & Assert
        Assert.Equal(money1, money2);
        Assert.True(money1 == money2);
        Assert.False(money1 != money2);
    }
    [Fact]
    public void Should_NotBeEqual_When_AmountsAreDifferent()
    {
        // Arrange
        var money1 = Money.From(100m);
        var money2 = Money.From(200m);
        // Act & Assert
        Assert.NotEqual(money1, money2);
        Assert.True(money1 != money2);
        Assert.False(money1 == money2);
    }

    [Fact]
    public void Should_KeepZero_When_AmountIsZero()
    {
        // Arrange
        var money = Money.From(0m);


        // Act & Assert
        Assert.Equal(0m, money.Value);


    }

    [Fact]
    public void Should_Handle_Null_Comparison()
    {
        // Arrange
        Money? nullMoney = null;
        var money = Money.From(100m);

        // Act & Assert
        Assert.True(nullMoney == null);
        Assert.False(nullMoney != null);

        Assert.False(nullMoney == money);
        Assert.True(nullMoney != money);
    }


    [Fact]
    public void Should_Return_Exception_When_Value_Is_Null()
    {
        // Arrange
        Money? price = null;

        // Act     
        // Assert
        Assert.Throws<MoneyException>(() => price + Money.From(100m));

    }


    [Fact]
    public void Should_Return_Amount_When_Querying_Money_Value()
    {
        // Arrange
        var price = Money.From(50m);

        // Act
        price += Money.From(10m);
        price -= Money.From(70m);

        var total = price.Value + 100m;

        // Assert   
        Assert.Equal(-10m, price.Value);

        Assert.NotEqual(total, price.Value);

    }

    [Fact]
    public void Should_Return_Sum_When_Value_Is_Not_Null()
    {
        // Arrange
        Money? price = Money.From(50m);

        // Act     
        var result = price + Money.From(100m);

        // Assert
        Assert.Equal(Money.From(150m), result);
    }


    [Fact]
    public void Should_Return_Subtract_When_Value_Is_Not_Null()
    {
        // Arrange
        Money? price = Money.From(50m);

        // Act     
        var result = price - Money.From(100m);

        // Assert
        Assert.Equal(Money.From(-50m), result);
    }

    [Fact]
    public void Should_Multiply_Money_By_Decimal()
    {
        // Arrange
        Money price = Money.From(50m);
        var multiplier = 17.6255m;
        var total = 50m * multiplier;

        // Act     
        var result = price * multiplier;

        // Assert
        Assert.Equal(Money.From(total)  , result);
    }

    [Fact]
    public void Should_Return_Exceptions_When_Multiplying_Money_By_Null_Decimal()
    {
        // Arrange
        Money? price = null;
        var multiplier = 17.6255m;

        // Act     
        // Assert
        Assert.Throws<MoneyException>(() => price * multiplier);

    }
    [Fact]
    public void Should_Division_Money_By_Decimal()
    {
        // Arrange
        Money price = Money.From(50m);
        var divisor = 2m;
        var total = 50m / divisor;

        // Act     
        var result = price / divisor;

        // Assert
        Assert.Equal(Money.From(total), result);
    }

    [Fact]
    public void Should_Return_Exceptions_When_Division_Money_By_Null_Decimal()
    {
        // Arrange
        Money? price = null;
        var divisor =200m;

        // Act     
        // Assert
        Assert.Throws<MoneyException>(() => price / divisor);

    }

    [Fact]
    public void Should_Return_Exceptions_When_Division_Money_By_Zero_Decimal()
    {
        // Arrange
        Money price = Money.From(200m);
        var divisor = 0m;
        // Act     
        // Assert
        Assert.Throws<MoneyException>(() => price / divisor).Equals("Não é possível realizar uma divisão monetária com valor zero.")    ;

    }
    [Fact]
    public void Should_Round_ToEven_When_RetainedDigitIsEven()
    {
        // Arrange  
        var money = Money.From(17.625m);
        //act
        var rounded = money.Round();
        //assert
        Assert.Equal(Money.From(17.62m), rounded);
        Assert.Equal(Money.From(17.625m), money);
    }


    [Fact]
    public void Should_Round_ToEven_When_RetainedDigitIsOdd()
    {
        //arrange       
        var money = Money.From(17.635m);
        //act
        var rounded = money.Round();
        //assert
        Assert.Equal(Money.From(17.64m), rounded);
        Assert.Equal(Money.From(17.635m), money);
    }
}
