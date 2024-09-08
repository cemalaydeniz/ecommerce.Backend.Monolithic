using ecommerce.Domain.Entities.Authentication;
using ecommerce.Test.Utility.Entities.Common.ValueObjects;

namespace ecommerce.Test.Utility.Entities.Authentication
{
    public static class UserLoginGenerator
    {
        public static UserLogin Generate()
        {
            return new UserLogin()
            {
                Id = Guid.NewGuid(),
                RefreshToken = TokenGenerator.Generate(),
                IpAddress = IpAddressGenerator.Generate(),
                DeviceInformation = StringGenerator.Generate()
            };
        }
    }
}
