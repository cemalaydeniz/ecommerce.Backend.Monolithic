#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Entities.Authentication.ValueObjects
{
    public class CreditCardInformation : ValueObject
    {
        public string? CardHolderNameEncrypted { get; set; }
        public string? CardNumberEncrypted { get; set; }
        public string? ExpirationDateEncrypted { get; set; }
        public string? CvvCodeEncrypted { get; set; }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return CardHolderNameEncrypted;
            yield return CardNumberEncrypted;
            yield return ExpirationDateEncrypted;
            yield return CvvCodeEncrypted;
        }
    }
}
