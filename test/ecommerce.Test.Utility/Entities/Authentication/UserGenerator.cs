using ecommerce.Domain.Entities.Authentication;

namespace ecommerce.Test.Utility.Entities.Authentication
{
    public static class UserGenerator
    {
        public static User Generate()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Email = Guid.NewGuid() + "@example.com",
                PasswordHash = StringGenerator.Generate(),
                PhoneNumber = StringGenerator.Generate(),
                SecurityStamp = Guid.NewGuid(),
                UserRoles = new HashSet<UserRole>(),
                UserLogins = new HashSet<UserLogin>(),
                UserTokens = new HashSet<UserToken>()
            };
        }
    }
}
