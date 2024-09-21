using ecommerce.Infrastructure.Options.Crypto;

namespace ecommerce.Test.Utility.Fixtures.Options
{
    public class PBKDF2Fixture
    {
        public CryptoOptions.PBKDF2 Pbkdf2Options { get; set; }

        public PBKDF2Fixture()
        {
            Pbkdf2Options = ConfigurationHelper.GetOption<CryptoOptions.PBKDF2>($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.PBKDF2)}")!;
        }
    }
}
