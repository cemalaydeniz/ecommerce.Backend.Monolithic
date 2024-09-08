using ecommerce.Domain.Entities.Authentication.ValueObjects;

namespace ecommerce.Test.Utility.Entities.Authentication.ValueObjects
{
    public static class CreditCardInformationGenerator
    {
        public static CreditCardInformation Generate()
        {
            return new CreditCardInformation()
            {
                CardHolderNameEncrypted = StringGenerator.Generate(),
                CardNumberEncrypted = StringGenerator.Generate(),
                ExpirationDateEncrypted = StringGenerator.Generate(),
                CvvCodeEncrypted = StringGenerator.Generate()
            };
        }
    }
}
