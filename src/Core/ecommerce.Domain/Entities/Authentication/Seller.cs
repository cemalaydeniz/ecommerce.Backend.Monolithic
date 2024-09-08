#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Domain.Entities.Authentication.ValueObjects;

namespace ecommerce.Domain.Entities.Authentication
{
    public class Seller : User
    {
        public string? BusinessName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public string? TinNumber { get; set; }
        public string? VatNumber { get; set; }
        public CreditCardInformation CreditCardInformation { get; set; }
        public EAccountStatus AccountStatus { get; set; }

        // Relations
        public Guid? BusinessAddressId { get; set; }
        public Address? BusinessAddress { get; set; }
        public Guid? BillingAddressId { get; set; }
        public Address? BillingAddress { get; set; }
        public ICollection<SellerUploadedFile> UploadedFiles { get; set; }
    }
}
