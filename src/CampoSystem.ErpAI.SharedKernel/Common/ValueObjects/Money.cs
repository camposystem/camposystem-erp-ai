using CampoSystem.ErpAI.SharedKernel.Common.Exceptions;

namespace CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    private decimal AmountIntermediate { get; }

    public decimal Amount => RoundValue(AmountIntermediate);

    private Money(decimal value) => AmountIntermediate = value;

    public static Money From(decimal value) => new(value);

    public static bool operator ==(Money? left, Money? right) => left?.Equals(right) ?? right is null;

    public static bool operator !=(Money? left, Money? right) => !(left == right);


    public bool Equals(Money? other) => other != null && Amount== other.Amount;

    public override bool Equals(object? obj) => Equals(obj as Money);

    public override int GetHashCode() => Amount.GetHashCode();

    private static decimal RoundValue(decimal value) => Math.Round(value, 2, MidpointRounding.ToEven);

    public static Money operator +(Money? left, Money? right)
    {
        if (left is null || right is null)
        {
            throw new MoneyException(
                 "Não é possível realizar uma operação monetária com valor nulo.");
        }

        return Money.From(left.AmountIntermediate + right.AmountIntermediate);
    }

    public static Money operator -(Money? left, Money? right)
    {
        if (left is null || right is null)
        {
            throw new MoneyException(
                 "Não é possível realizar uma operação monetária com valor nulo.");
        }

        return Money.From(left.AmountIntermediate - right.AmountIntermediate);
    }

    public static Money operator *(Money? left, decimal right)
    {
        if (left is null )
        {
            throw new MoneyException(
                 "Não é possível realizar uma operação monetária com valor nulo.");
        }

        return Money.From(left.AmountIntermediate * right);
    }

    public static Money operator /(Money? left, decimal right)
    {
        if (left is null)
        {
            throw new MoneyException(
                 "Não é possível realizar uma operação monetária com valor nulo.");
        }

        if (right == 0m)
        {
            throw new MoneyException(
                 "Não é possível realizar uma divisão monetária com valor zero.");
        }

        return Money.From(left.AmountIntermediate / right);
    }








}
