#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace ecommerce.Domain.Entities.Account
{
    public class Address
    {
        public string StreetLine1 { get; set; }
        public string? StreetLine2 { get; set; }
        public string StateOrProvince { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
