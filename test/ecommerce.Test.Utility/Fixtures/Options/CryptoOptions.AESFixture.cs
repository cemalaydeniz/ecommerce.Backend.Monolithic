using ecommerce.Infrastructure.Options.Crypto;

namespace ecommerce.Test.Utility.Fixtures.Options
{
    public class AESFixture
    {
        public CryptoOptions.AES AesOptions { get; set; }

        public AESFixture()
        {
            AesOptions = ConfigurationHelper.GetOption<CryptoOptions.AES>($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.AES)}")!;
        }
    }
}
