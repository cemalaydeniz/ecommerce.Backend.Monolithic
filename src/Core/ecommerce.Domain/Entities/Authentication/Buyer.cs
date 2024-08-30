#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Domain.Entities.Account;

namespace ecommerce.Domain.Entities.Authentication
{
    public class Buyer : User
    {
        public string? FullName { get; set; }

        // Relations
        public ICollection<Address> Addresses { get; set; }
    }
}
