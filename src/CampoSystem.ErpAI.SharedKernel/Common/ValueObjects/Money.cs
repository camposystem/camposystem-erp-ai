namespace CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

public sealed class Money : IEquatable<Money>
{

    public decimal Amount { get; }

    private Money(decimal amount) => Amount = amount;

    public static Money From(decimal amount) => new(amount);

    public bool Equals(Money? other)
    {
     return other != null && Amount == other.Amount;
    }

    public override bool Equals(object? obj) => Equals(obj as Money);

    public override int GetHashCode() => Amount.GetHashCode();
}
