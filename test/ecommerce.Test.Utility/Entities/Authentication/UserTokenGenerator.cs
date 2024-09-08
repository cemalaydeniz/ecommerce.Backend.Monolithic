using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Test.Utility.Entities.Common.ValueObjects;

namespace ecommerce.Test.Utility.Entities.Authentication
{
    public static class UserTokenGenerator
    {
        public static UserToken Generate()
        {
            return new UserToken()
            {
                Id = Guid.NewGuid(),
                Token = TokenGenerator.Generate(),
                Purpose = ETokenPurpose.ConfirmEmail
            };
        }
    }
}
