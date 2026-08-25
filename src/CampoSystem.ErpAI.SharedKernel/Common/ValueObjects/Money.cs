using CampoSystem.ErpAI.SharedKernel.Common.Exceptions;

namespace CampoSystem.ErpAI.SharedKernel.Common.ValueObjects;

public sealed class Money : IEquatable<Money>, IComparable<Money>
{
    /// <summary>
    /// The maximum or minimum amount allowed for the Money value object.
    /// </summary>
    private const decimal MaximumAmount = 9999999999999999.99m;

    private decimal AmountIntermediate { get; }

    public decimal Amount => RoundValue(AmountIntermediate);

    public bool IsNegative => Amount < 0m;


    public bool IsZero => Amount == 0m;

    public bool IsPositive => Amount >= 0m;

    private Money(decimal value) => AmountIntermediate = value;
    /// <summary>
    /// Creates a new instance of the Money value object from a decimal value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="MoneyOverflowException"></exception>
    public static Money From(decimal value)
    {
        var valueRounded = RoundValue(value);

        if (valueRounded > MaximumAmount || valueRounded < -MaximumAmount)
        {
            throw new MoneyOverflowException("O valor monetário não pode ser maior que 9999999999999999.99 ou menor que -9999999999999999.99 .");
        }

        return new Money(value);
    }

    public static bool operator ==(Money? left, Money? right) => left?.Equals(right) ?? right is null;

    public static bool operator !=(Money? left, Money? right) => !(left == right);


    public bool Equals(Money? other) => other != null && Amount == other.Amount;

    public override bool Equals(object? obj) => Equals(obj as Money);

    public override int GetHashCode() => Amount.GetHashCode();

    private static decimal RoundValue(decimal value) => Math.Round(value, 2, MidpointRounding.ToEven);

    public int CompareTo(Money? other) => other != null ? Amount.CompareTo(other.Amount) : 1;

    public static bool operator <(Money? left, Money? right) => left is null ? right is not null : left.CompareTo(right) < 0;

    public static bool operator >(Money? left, Money? right) => left is null ? false : left.CompareTo(right) > 0;

    public static bool operator <=(Money? left, Money? right) => left is null || left.CompareTo(right) <= 0;

    public static bool operator >=(Money? left, Money? right) => left is null ? right is null : left.CompareTo(right) >= 0;

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
        if (left is null)
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
