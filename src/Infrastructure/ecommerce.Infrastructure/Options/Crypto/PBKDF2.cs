namespace ecommerce.Infrastructure.Options.Crypto
{
    public partial class CryptoOptions
    {
        public class PBKDF2
        {
            public int? SaltSize { get; set; }
            public int? KeySize { get; set; }
            public int? Iterations { get; set; }
        }
    }
}
