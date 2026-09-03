
using CampoSystem.ErpAI.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Finances.Exceptions;

namespace CampoSystem.ErpAI.Domain.Finances.ValueObjects;


public class Period     
{
    private Period(int value, TimeUnit timeUnit)
    {
        Value = value;
        TimeUnit = timeUnit;
    }

    public decimal Value { get; }
    public TimeUnit TimeUnit { get; }

    public static Period From(int value, TimeUnit timeUnit)
    {
        if (value <= 0)
        {
            throw new PeriodException("Period cannot be zero or negative.");
        }
        return new Period(value, timeUnit);
    }
}
