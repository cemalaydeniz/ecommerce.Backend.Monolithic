using ecommerce.Infrastructure.Options.Crypto;

namespace ecommerce.Test.Utility.Fixtures.Options
{
    public class AESFixture
    {
        public CryptoOptions.AES aesOptions { get; set; }

        public AESFixture()
        {
            aesOptions = ConfigurationsHelper.GetOption<CryptoOptions.AES>($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.AES)}")!;
        }
    }
}
