using ecommerce.Domain.Entities.Account;

namespace ecommerce.Test.Utility.Entities.Account
{
    public static class AddressGenerator
    {
        public static Address Generate()
        {
            return new Address()
            {
                Id = Guid.NewGuid(),
                Title = StringGenerator.Generate(),
                StreetLine1 = StringGenerator.Generate(),
                StreetLine2 = StringGenerator.Generate(),
                StateOrProvince = StringGenerator.Generate(),
                ZipCode = StringGenerator.Generate(),
                City = StringGenerator.Generate(),
                Country = StringGenerator.Generate()
            };
        }
    }
}
