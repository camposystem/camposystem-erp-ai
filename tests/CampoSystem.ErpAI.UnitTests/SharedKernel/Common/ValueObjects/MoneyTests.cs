using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.SharedKernel.Common.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Money_ShouldBeEqual_WhenAmountsAreEqual()
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
    public void Money_ShouldNotBeEqual_WhenAmountsAreDifferent()
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
    public void Money_ShouldBeNull_WhenAmountIsZero()
    {
        // Arrange
        var money = Money.From(0m);


        // Act & Assert
        Assert.Equal(0m, money.Amount);


    }

    [Fact]
    public void Money_ShouldHandleNullComparison()
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
}