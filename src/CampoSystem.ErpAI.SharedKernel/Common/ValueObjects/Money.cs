namespace CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

public sealed class Money
{

    public decimal Amount { get; }

    private Money(decimal amount) => Amount = amount;

    public static Money From(decimal amount) => new(amount);

}
