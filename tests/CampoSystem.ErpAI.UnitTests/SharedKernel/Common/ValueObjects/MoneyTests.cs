using CampoSystem.ErpAI.SharedKernel.Common.Exceptions;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.SharedKernel.Common.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Should_BeEqual_When_AmountsAreEqual()
    {
        // Arrange
        var money1 = Money.From(100.125m);
        var money2 = Money.From(100.125m);
        // Act & Assert
        Assert.Equal(money1, money2);
        Assert.True(money1 == money2);
        Assert.False(money1 != money2);
    }

    [Fact]
    public void Should_BeEqual_When_AmountsAreEquivalents()
    {
        // Arrange
        var money1 = Money.From(100.124m);
        var money2 = Money.From(100.125m);
        // Act & Assert
        Assert.Equal(money1, money2);
        Assert.True(money1 == money2);
        Assert.False(money1 != money2);
    }

    [Fact]
    public void Should_NotBeEqual_When_AmountsAreDifferent()
    {
        // Arrange
        var money1 = Money.From(100.125m);
        var money2 = Money.From(100.126m);
        // Act & Assert
        Assert.NotEqual(money1, money2);
        Assert.True(money1 != money2);
        Assert.False(money1 == money2);
    }

    [Fact]
    public void Should_KeepZero_When_AmountIsZero()
    {
        // Arrange
        var money = Money.From(0.000m);


        // Act & Assert
        Assert.Equal(0m, money.Amount);


    }

    [Fact]
    public void Should_Handle_Null_Comparison()
    {
        // Arrange
        Money? nullMoney = null;
        var money = Money.From(100.125m);

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
        Assert.Throws<MoneyException>(() => price + Money.From(100.125m));

    }


    [Fact]
    public void Should_Return_Amount_When_Querying_Money_Value()
    {
        // Arrange
        var price = Money.From(50.125m);

        // Act
        price += Money.From(10.125m);
        price -= Money.From(70.125m);

        var total = price + Money.From(100.125m);

        // Assert   
        Assert.Equal(-9.88m, price.Amount);

        Assert.NotEqual(total, price);

    }

    [Fact]
    public void Should_Return_Sum_When_Value_Is_Not_Null()
    {
        // Arrange
        Money? price = Money.From(50.135m);

        // Act     
        var result = price + Money.From(100.125m);

        // Assert
        Assert.Equal(Money.From(150.26m), result);
    }


    [Fact]
    public void Should_Return_Subtract_When_Value_Is_Not_Null()
    {
        // Arrange
        Money? price = Money.From(50.135m);

        // Act     
        var result = price - Money.From(100.125m);
        var amountIntermidiatePrivate = Money.From(0.010m) + result;

        // Assert
        Assert.Equal(Money.From(-49.990m), result);
        Assert.Equal(Money.From(-49.980m), amountIntermidiatePrivate);
    }

    [Fact]
    public void Should_Multiply_Money_By_Decimal()
    {
        // Arrange
        Money priceAmount = Money.From(50.135m);
        Money priceAmountB = Money.From(50.14m);
        var multiplier = 17.6255m;
        var result = 50.135m * multiplier;

        // Act    
        var calculo = priceAmount * multiplier;
        var calculoB = priceAmountB * multiplier;

        // Assert
        Assert.Equal(Money.From(result), calculo);
        Assert.Equal(priceAmount, priceAmountB);
        Assert.NotEqual(calculo, calculoB);
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
        Money price = Money.From(50.135m);
        Money priceB = Money.From(50.144m);
        var divisor = 3.05m;

        // Act     
        var operacaoA = price / divisor;
        var operacaoB = priceB / divisor;
        var expectedA = (50.135m / divisor);
        var expectedB = (50.144m / divisor);

        // Assert
        Assert.Equal(Money.From(expectedA), operacaoA);
        Assert.Equal(Money.From(expectedB), operacaoB);
    }

    [Fact]
    public void Should_Return_Exceptions_When_Division_Money_By_Null_Decimal()
    {
        // Arrange
        Money? price = null;
        var divisor = 20.06m;

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
        Assert.Throws<MoneyException>(() => price / divisor).Equals("Não é possível realizar uma divisão monetária com valor zero.");

    }
    [Fact]
    public void Should_Round_ToEven_When_RetainedDigitIsEven()
    {
        // Arrange  
        var money = Money.From(17.625m);
        //act
        //assert
        Assert.Equal(17.62m, money.Amount);
    }


    [Fact]
    public void Should_Round_ToEven_When_RetainedDigitIsOdd()
    {
        //arrange       
        var money = Money.From(17.635m);
        //act
        //assert
        Assert.Equal(17.64m, money.Amount);
    }

    [Fact]
    public void Should_Create_Money_When_Value_Is_At_Maximum_Limit()
    {
        //arrange       
        var value = 9999999999999999.99m;

        //act
        var money = Money.From(value);

        //assert
        Assert.NotNull(money);
    }

    [Fact]
    public void Should_Throw_MoneyOverflowException_When_Value_Is_Greater_Than_Maximum_Limit()
    {
        //arrange       
        var value = 10000000000000000.00m;

        //act & assert
        Assert.Throws<MoneyOverflowException>(() => Money.From(value));
    }

    [Fact]
    public void Should_Create_Money_When_Value_Is_At_Minimum_Limit()
    {
        //arrange       
        var value = -9999999999999999.99m;

        //act
        var money = Money.From(value);

        //assert
        Assert.NotNull(money);
    }

    [Fact]
    public void Should_Create_Money_When_IntermediateValue_Is_At_Maximum_Limit()
    {
        var value = 9999999999999999.994m;

        var money = Money.From(value);

        Assert.Equal(Money.From(9999999999999999.99m), money);
    }

    [Fact]
    public void Should_Throw_MoneyOverflowException_When_Value_Is_Less_Than_Minimum_Limit()
    {
        //arrange       
        var value = -10000000000000000.00m;

        //act & assert
        Assert.Throws<MoneyOverflowException>(() => Money.From(value));
    }

    [Fact]
    public void Should_Throw_MoneyOverflowException_When_IntermediateValue_Rounds_Above_Maximum_Limit()
    {
        var value = 9999999999999999.995m;

        //act & assert
        Assert.Throws<MoneyOverflowException>(() => Money.From(value));
    }


    [Fact]
    public void Should_Throw_MoneyOverflowException_When_IntermediateValue_Rounds_Above_Minimum_Limit()
    {
        var value = -9999999999999999.995m;

        //act & assert
        Assert.Throws<MoneyOverflowException>(() => Money.From(value));
    }

    [Fact]
    public void Should_Money_Be_Considered_Smaller_When_Is_Less_Than_Other()
    {
        // Arrange
        Money money1 = Money.From(10.124m);
        Money money2 = Money.From(20.125m);

        // Act
        var isSmaller = money1 < money2;

        // Assert
        Assert.True(isSmaller);
    }

    [Fact]
    public void Should_Money_Be_Considered_Smaller_Or_Equal_When_Is_Less_or_Equal_Than_Other()
    {
        // Arrange
        Money money1 = Money.From(10.114m);
        Money money2 = Money.From(20.125m);
        Money money3 = Money.From(10.114m);

        // Act
        var isSmaller = money1 <= money2;
        var isEqual = money1 <= money3;

        // Assert
        Assert.True(isSmaller);
        Assert.True(isEqual);
    }

    [Fact]
    public void Should_Money_Be_Considered_Greater_When_Is_Greater_Than_Other()
    {
        // Arrange
        Money money1 = Money.From(20.125m);
        Money money2 = Money.From(10.124m);

        // Act
        var isGreater = money1 > money2;

        // Assert
        Assert.True(isGreater);
    }


    [Fact]
    public void Should_Money_Be_Considered_Greater_Or_Equal_When_Is_Greater_Or_Equal_Than_Other()
    {
        // Arrange
        Money money1 = Money.From(20.125m);
        Money money2 = Money.From(10.114m);
        Money money3 = Money.From(20.125m);

        // Act
        var isGreaterOrEqual = money1 >= money2;
        var isEqual = money1 >= money3;

        // Assert
        Assert.True(isGreaterOrEqual);
        Assert.True(isEqual);
    }
    [Fact]
    public void Should_Return_Zero_When_The_Equivalent_Value()
    {
        // Arrange
        Money money1 = Money.From(10.124m);
        Money money2 = Money.From(10.125m);

        // Act
        var comparison = money1.CompareTo(money2);

        // Assert
        Assert.True(comparison == 0);
        Assert.Equal(money1, money2);

    }

    [Fact]
    public void Should_Return_Positive_Value_When_Money_Is_Greater_Than_Other()
    {
        // Arrange
        Money money1 = Money.From(10.129m);
        Money money2 = Money.From(10.124m);

        // Act
        var comparison = money1.CompareTo(money2);

        // Assert
        Assert.True(comparison > 0);
    }
}

