using ecommerce.Infrastructure.Options.Crypto;

namespace ecommerce.Test.Utility.Fixtures.Options
{
    public class PBKDF2Fixture
    {
        public CryptoOptions.PBKDF2 pbkdf2Options { get; set; }

        public PBKDF2Fixture()
        {
            pbkdf2Options = ConfigurationsHelper.GetOption<CryptoOptions.PBKDF2>($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.PBKDF2)}")!;
        }
    }
}
