using CampoSystem.ErpAI.Domain.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Finances;
using CampoSystem.ErpAI.Domain.Finances.Contracts;
using CampoSystem.ErpAI.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Finances.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.Domain.Finances;

public class InterestCompoundCalculatorTests
{
    [Theory]
    [InlineData(100, 10, 2, TimeUnit.Months, 121)]
    [InlineData(100, 25, 2, TimeUnit.Months, 156.25)]
    [InlineData(100, 50, 2, TimeUnit.Months, 225)]
    [InlineData(100, 150, 2, TimeUnit.Months, 625)]
    public void Given_A_Money_Positive_Interest_Rate_And_Period_Should_Calculate_Compound_Interest(int capital, int percentage, int period, TimeUnit timeUnit, decimal expected)
    {   // Arrange
        var moneyCapital = Money.From((decimal)capital);
        IInterestCalculator calculator = new InterestCompoundCalculator();
        Rate rate = Rate.From((decimal)percentage, timeUnit);
        Period periodCalc = Period.From(period, timeUnit);

        // Act
        var result = calculator.Calculate(moneyCapital, rate, periodCalc);

        // Assert
        Assert.Equal(Money.From((decimal)expected), result);
    }

    [Theory]
    [InlineData(100.0, 10.0, RateType.Effective, TimeUnit.Years, 1.0, TimeUnit.Months, 100.80)]
    public void Given_A_Money_Positive_Interest_Rate_With_A_Different_Period_Should_Calculate_Compound_Interest(
    decimal capital, decimal percentage, RateType rateType, TimeUnit rateTimeUnit, decimal period, TimeUnit periodTimeUnit, decimal expected)
    {   // Arrange
        var moneyCapital = Money.From((decimal)capital);
        IInterestCalculator calculator = new InterestCompoundCalculator();
        Rate rate = Rate.From((decimal)percentage, rateTimeUnit, rateType);
        Period periodCalc = Period.From((int)period, periodTimeUnit);

        // Act
        var result = calculator.Calculate(moneyCapital, rate, periodCalc);

        // Assert
        Assert.Equal(Money.From((decimal)expected), result);
    }

}
