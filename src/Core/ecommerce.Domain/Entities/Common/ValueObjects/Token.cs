#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Entities.Common.ValueObjects
{
    public class Token : ValueObject
    {
        public string ValueEncypted { get; set; }
        public DateTime ExpirationDate { get; set; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return ValueEncypted;
            yield return ExpirationDate;
        }
    }
}
