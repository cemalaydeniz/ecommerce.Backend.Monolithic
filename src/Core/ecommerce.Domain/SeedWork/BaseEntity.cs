#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace ecommerce.Domain.SeedWork
{
    public abstract class BaseEntity<TKey> : IEquatable<BaseEntity<TKey>>
        where TKey : notnull
    {
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; private set; }

        public override bool Equals(object? obj)
        {
            return obj is BaseEntity<TKey> other && Id.Equals(other.Id);
        }

        public bool Equals(BaseEntity<TKey>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            if (left == null && right == null) return true;
            if (left != null && right != null) return left.Equals(right);
            return false;
        }

        public static bool operator !=(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
