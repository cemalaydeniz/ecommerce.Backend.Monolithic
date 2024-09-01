namespace ecommerce.Domain.SeedWork
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }

        public static bool operator== (ValueObject? left, ValueObject? right)
        {
            if (left is null && right is null) return true;
            if (left is not null && right is not null) return left.Equals(right);
            return false;
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents().Select(x => x?.GetHashCode() ?? 0).Aggregate((x, y) => x ^ y);
        }
    }
}
