namespace ecommerce.Infrastructure.Options.Crypto
{
    public partial class CryptoOptions
    {
        public class AES
        {
            public string? Key { get; set; }
            public int? NonceSize { get; set; }
            public int? TagSize { get; set; }
        }
    }
}
