using CampoSystem.ErpAI.SharedKernel.Common.Enums;
using CampoSystem.ErpAI.SharedKernel.Common.Exceptions;

namespace CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;


public class Rate
{
    private Rate(decimal value, TimeUnit timeUnit)
    {
        Value = value;
        TimeUnit = timeUnit;
    }

    public decimal Value { get; }
    public TimeUnit TimeUnit { get; }

    public static Rate From(decimal value, TimeUnit timeUnit)
    {
        if (value <= 0)
        {
            throw new RateException("Rate cannot be zero or negative.");
        }
        return new Rate(value, timeUnit);
    }
}
