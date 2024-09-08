using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication;

namespace ecommerce.Test.Utility.Entities.Authentication
{
    public static class BuyerGenerator
    {
        public static Buyer Generate()
        {
            return new Buyer()
            {
                Id = Guid.NewGuid(),
                FullName = StringGenerator.Generate(),
                Email = Guid.NewGuid() + "@example.com",
                PasswordHash = StringGenerator.Generate(),
                PhoneNumber = StringGenerator.Generate(),
                SecurityStamp = Guid.NewGuid(),
                UserRoles = new HashSet<UserRole>(),
                UserLogins = new HashSet<UserLogin>(),
                UserTokens = new HashSet<UserToken>(),
                Addresses = new HashSet<Address>()
            };
        }
    }
}
