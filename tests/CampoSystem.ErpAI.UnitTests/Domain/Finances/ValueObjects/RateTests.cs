using CampoSystem.ErpAI.Domain.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Domain.Finances.Enums.Extensions;
using CampoSystem.ErpAI.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Finances.Exceptions;
using CampoSystem.ErpAI.Domain.Finances.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.Domain.Finances.ValueObjects;

public class RateTests
{
    [Fact]
    public void Given_A_Positive_Rate_Should_Create_Rate()
    {
        // Arrange
        var value = 10m;

        // Act
        var rate = Rate.From(value, TimeUnit.Days);

        // Assert
        Assert.NotNull(rate);
    }
    [Fact]
    public void Given_A_Positive_Rate_Should_Create_Rate_With_TimeUnit()
    {
        // Arrange
        var value = 10m;
        var timeUnit = TimeUnit.Months;
        // Act
        var rate = Rate.From(value, timeUnit);

        // Assert
        Assert.Equal(value, rate.Value);
        Assert.Equal(timeUnit, rate.TimeUnit);
    }

    [Fact]
    public void Given_A_Zero_Rate_Should_Throw_Exception()
    {
        // Arrange
        var value = 0m;

        // Act & Assert
        Assert.Throws<RateException>(() => Rate.From(value, TimeUnit.Days));
    }

    [Fact]
    public void Given_A_Negative_Rate_Should_Throw_Exception()
    {
        // Arrange
        var value = -10m;

        // Act & Assert
        Assert.Throws<RateException>(() => Rate.From(value, TimeUnit.Days));
    }


    [Theory]
    [InlineData(TimeUnit.Days, 1)]
    [InlineData(TimeUnit.Months, 30)]
    [InlineData(TimeUnit.Years, 360)]
    public void TimeUnit_Should_Return_Correct_Factor(TimeUnit timeUnit,int expected)
    {   
        // Arrange & Act
        var factor = timeUnit.ToFactor();

        // Assert
        Assert.Equal(expected, factor);
    }

    

    [Theory]
    [InlineData(10.0, TimeUnit.Years, RateType.Effective, 0.02647855, TimeUnit.Days)]
    [InlineData(10.0, TimeUnit.Years, RateType.Effective, 0.79741404, TimeUnit.Months)]
    [InlineData(10.0, TimeUnit.Years, RateType.Effective, 10.00000000, TimeUnit.Years)]
    [InlineData(2.0, TimeUnit.Months, RateType.Effective, 2.00000000, TimeUnit.Months)]
    [InlineData(0.00000125, TimeUnit.Days, RateType.Effective, 0.00000125, TimeUnit.Days)]
    public void Given_An_Effective_Rate_When_Converted_To_A_TimeUnit_Should_Return_The_Equivalent_Rate(
        decimal value, TimeUnit timeUnit, RateType rateType, decimal expected, TimeUnit expectedTimeUnit)
    {
        // Arrange
        var rate = Rate.From(value, timeUnit, rateType);


        // Act
        var result = rate.ConvertTo(expectedTimeUnit);

        // Assert
        Assert.Equal(expected, result.Value);
        Assert.Equal(expectedTimeUnit, result.TimeUnit);
    }


    [Theory]
    [InlineData(12.0, TimeUnit.Years, RateType.Nominal, 0.03333333, TimeUnit.Days)]
    [InlineData(12.0, TimeUnit.Years, RateType.Nominal, 1.00000000, TimeUnit.Months)]
    [InlineData(12.0, TimeUnit.Years, RateType.Nominal, 12.00000000, TimeUnit.Years)]
    [InlineData(1.0, TimeUnit.Months, RateType.Nominal, 1.00000000, TimeUnit.Months)]
    [InlineData(0.03333333, TimeUnit.Days, RateType.Nominal, 0.03333333, TimeUnit.Days)]
    public void Given_An_Nominal_Rate_When_Converted_To_A_TimeUnit_Should_Return_The_Equivalent_Rate(
        decimal value, TimeUnit timeUnit, RateType rateType, decimal expected, TimeUnit expectedTimeUnit)
    {
        // Arrange
        var rate = Rate.From(value, timeUnit, rateType);


        // Act
        var result = rate.ConvertTo(expectedTimeUnit);

        // Assert
        Assert.Equal(expected, result.Value);
        Assert.Equal(expectedTimeUnit, result.TimeUnit);
    }

}
