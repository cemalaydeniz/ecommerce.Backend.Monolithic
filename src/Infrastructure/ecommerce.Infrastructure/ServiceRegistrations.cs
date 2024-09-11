using ecommerce.Application.Abstractions.Crypto;
using ecommerce.Infrastructure.Crypto;
using ecommerce.Infrastructure.Exceptions;
using ecommerce.Infrastructure.Options.Crypto;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static void AddInfrastructureServices(this IServiceCollection services,
            CryptoOptions.PBKDF2? pbkdf2,
            CryptoOptions.AES? aes)
        {
            CheckPBKDF2(pbkdf2);
            CheckAES(aes);

            services.AddSingleton<IHashOperations, PBKDF2>();
            services.AddSingleton<IEncryptionOperations, AES>();
        }

        private static void CheckPBKDF2(CryptoOptions.PBKDF2? pbkdf2)
        {
            if (pbkdf2 == null ||
                pbkdf2.Iterations == null ||
                pbkdf2.KeySize == null ||
                pbkdf2.SaltSize == null)
                throw new ConfigurationException($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.PBKDF2)} configuration is not found or is not set properly");
        }

        private static void CheckAES(CryptoOptions.AES? aes)
        {
            if (aes == null ||
                aes.Key == null ||
                aes.NonceSize == null ||
                aes.TagSize == null)
                throw new ConfigurationException($"{nameof(CryptoOptions)}:{nameof(CryptoOptions.AES)} configuration is not found or is not set properly");
        }
    }
}
