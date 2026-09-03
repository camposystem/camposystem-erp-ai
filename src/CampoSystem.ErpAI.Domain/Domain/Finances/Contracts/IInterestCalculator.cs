using CampoSystem.ErpAI.Domain.Finances.ValueObjects;
using CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

namespace CampoSystem.ErpAI.Domain.Finances.Contracts;

public interface IInterestCalculator
{
    Money Calculate(Money capital,Rate rate,Period period);
}
