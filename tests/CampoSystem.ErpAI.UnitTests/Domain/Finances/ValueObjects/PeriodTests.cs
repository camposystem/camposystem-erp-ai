using CampoSystem.ErpAI.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Finances.Exceptions;
using CampoSystem.ErpAI.Domain.Finances.ValueObjects;

namespace CampoSystem.ErpAI.UnitTests.Domain.Finances.ValueObjects;

public class PeriodTests
{
    [Fact]
    public void Given_A_Positive_Period_Should_Create_Period_With_TimeUnit()
    {
        // Arrange
        var value = 3;
        var timeUnit = TimeUnit.Days;

        // Act
        var period = Period.From(value, timeUnit);

        // Assert
        Assert.Equal(value, period.Value);
        Assert.Equal(timeUnit, period.TimeUnit);    
    }
    [Fact]
    public void Given_A_Zero_Period_Should_Throw_Exception()
    {
        // Arrange
        var value = 0;

        // Act & Assert
        Assert.Throws<PeriodException>(() => Period.From(value, TimeUnit.Days));
    }

    [Fact]
    public void Given_A_Negative_Period_Should_Throw_Exception()
    {
        // Arrange
        var value = -10 ;

        // Act & Assert
        Assert.Throws<PeriodException>(() => Period.From(value, TimeUnit.Days));
    }
}
