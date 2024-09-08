using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Test.Utility.Entities.Authentication.ValueObjects;

namespace ecommerce.Test.Utility.Entities.Authentication
{
    public static class SellerGenerator
    {
        public static Seller Generate()
        {
            return new Seller()
            {
                Id = Guid.NewGuid(),
                BusinessName = StringGenerator.Generate(),
                ContactName = StringGenerator.Generate(),
                ContactEmail = Guid.NewGuid() + "@example.com",
                ContactPhoneNumber = StringGenerator.Generate(),
                TinNumber = StringGenerator.Generate(),
                VatNumber = StringGenerator.Generate(),
                CreditCardInformation = CreditCardInformationGenerator.Generate(),
                Email = Guid.NewGuid() + "@example.com",
                PasswordHash = StringGenerator.Generate(),
                PhoneNumber = StringGenerator.Generate(),
                SecurityStamp = Guid.NewGuid(),
                AccountStatus = EAccountStatus.Pending,
                UserRoles = new HashSet<UserRole>(),
                UserLogins = new HashSet<UserLogin>(),
                UserTokens = new HashSet<UserToken>(),
                UploadedFiles = new HashSet<SellerUploadedFile>()
            };
        }
    }
}
