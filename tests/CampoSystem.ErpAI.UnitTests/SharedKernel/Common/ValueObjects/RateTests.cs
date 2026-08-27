using CampoSystem.ErpAI.SharedKernel.Common.Enums;
using CampoSystem.ErpAI.SharedKernel.Common.Exceptions;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.SharedKernel.Common.ValueObjects;

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
        Assert.Equal(rate.Value, value);
        Assert.Equal(rate.TimeUnit, timeUnit);
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
        Assert.Throws<RateException>(() => Rate.From(value, TimeUnit.Days   ));
    }
}
