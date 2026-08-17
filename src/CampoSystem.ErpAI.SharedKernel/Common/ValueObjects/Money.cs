using CampoSystem.ErpAI.SharedKernel.Common.Exceptions;

namespace CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

public sealed class Money : IEquatable<Money>
{

    private decimal Amount { get; }

    public decimal Value => Amount;

    private Money(decimal amount) =>
        Amount = amount;

    public static Money From(decimal amount) =>
        new(amount);

    public static bool operator ==(Money? left, Money? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Money? left, Money? right) =>
        !(left == right);

    public static Money operator +(Money? left, Money? right)
    {
        if (left is null || right is null)
        {
            throw new MoneyException(
                 "Não é possível realizar uma operação monetária com valor nulo.");
        }

        return Money.From(left.Amount + right.Amount);
    }


    public static Money operator -(Money? left, Money? right)
    {
        if (left is null || right is null)
        {
            throw new MoneyException(
                 "Não é possível realizar uma operação monetária com valor nulo.");
        }

        return Money.From(left.Amount - right.Amount);
    }


    public bool Equals(Money? other) =>
        other != null && Amount == other.Amount;

    public override bool Equals(object? obj) =>
        Equals(obj as Money);

    public override int GetHashCode() =>
        Amount.GetHashCode();
}
