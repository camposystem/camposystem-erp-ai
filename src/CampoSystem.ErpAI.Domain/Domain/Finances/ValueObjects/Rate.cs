using CampoSystem.ErpAI.Domain.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Domain.Finances.Enums.Extensions;
using CampoSystem.ErpAI.Domain.Finances.Enums;
using CampoSystem.ErpAI.Domain.Finances.Exceptions;

namespace CampoSystem.ErpAI.Domain.Finances.ValueObjects;


public class Rate
{
    private const int CASAS_DECIMAIS_PADRAO = 8;
    private decimal RateBase { get; }
    public decimal Value { get; }
    public TimeUnit TimeUnit { get; }
    public RateType Type { get; }

    private Rate(decimal value, TimeUnit timeUnit, RateType type)
    {
        Value = decimal.Round(value, CASAS_DECIMAIS_PADRAO);
        TimeUnit = timeUnit;
        Type = type;
        RateBase = ComputeRateBase(value, timeUnit, type);
    }


    public static Rate From(decimal value, TimeUnit timeUnit, RateType type = RateType.Nominal)
    {
        if (value <= 0)
        {
            throw new RateException("Rate cannot be zero or negative.");
        }
        return new Rate(value, timeUnit, type);
    }

    public Rate ConvertTo(TimeUnit timeUnit)
    {

        if (timeUnit == TimeUnit.Days) return Rate.From(RateBase * 100, timeUnit, Type);

        if (RateType.Effective == Type) return Rate.From((decimal)(Math.Pow(1 + (double)RateBase, timeUnit.ToFactor()) - 1) * 100, timeUnit, Type);

        if (RateType.Nominal == Type) return Rate.From((RateBase * timeUnit.ToFactor()) * 100, timeUnit, Type);

        throw new RateException("Invalid rate type.");
    }


    private decimal ComputeRateBase(decimal value, TimeUnit timeUnit, RateType type)
    {
        if (type == RateType.Effective) return (decimal)Math.Pow(1 + (double)value / 100.0, 1.0 / timeUnit.ToFactor()) - 1;

        if (type == RateType.Nominal) return (value / 100.0m) / timeUnit.ToFactor();

        throw new RateException("Invalid rate type or time unit combination.");

    }

}