using CampoSystem.ErpAI.Domain.Finances.Contracts;
using CampoSystem.ErpAI.Domain.Finances.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.Domain.Finances;

public class InterestCompoundCalculator : IInterestCalculator
{
    public Money Calculate(Money capital, Rate rate, Period period)
    {
        if(rate.TimeUnit != period.TimeUnit)
        {
            rate = rate.ConvertTo(period.TimeUnit);
        }

        decimal interest = capital.Amount * ((decimal)Math.Pow((double)(1m + rate.Value / 100m), (double)period.Value) - 1);

        return Money.From(capital.Amount + interest);
    }

}
