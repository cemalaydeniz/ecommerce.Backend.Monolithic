using ecommerce.Domain.Entities.Common.ValueObjects;

namespace ecommerce.Test.Utility.Entities.Common.ValueObjects
{
    public static class TokenGenerator
    {
        public static Token Generate()
        {
            return new Token()
            {
                ValueEncypted = StringGenerator.Generate(),
                ExpirationDate = DateTime.UtcNow.AddDays(1)
            };
        }
    }
}
