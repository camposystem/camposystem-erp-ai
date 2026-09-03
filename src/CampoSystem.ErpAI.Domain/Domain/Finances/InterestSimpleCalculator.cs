using CampoSystem.ErpAI.Domain.Finances.Contracts;
using CampoSystem.ErpAI.Domain.Finances.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.Domain.Finances;

public class InterestSimpleCalculator: IInterestCalculator
{
    public Money Calculate(Money capital, Rate rate, Period period)
    {
        if(rate.TimeUnit != period.TimeUnit)
        {
            rate = rate.ConvertTo(period.TimeUnit);
        }

        decimal interest = capital.Amount * (rate.Value / 100m) * period.Value;
        
        return Money.From(capital.Amount + interest);
    }

}
