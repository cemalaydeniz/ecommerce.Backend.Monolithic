using ecommerce.Application.Abstractions.Crypto;
using ecommerce.Infrastructure.Options.Crypto;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ecommerce.Infrastructure.Crypto
{
    public class AES : IEncryptionOperations
    {
        private readonly CryptoOptions.AES _aesOptions;

        public AES(IOptions<CryptoOptions.AES> aesOptions)
        {
            _aesOptions = aesOptions.Value;
        }

        public string EncryptValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{nameof(value)} cannot be null or empty");

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);

            byte[] nonce = new byte[_aesOptions.NonceSize!.Value];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(nonce);
            }

            byte[] cipherText = new byte[valueBytes.Length];
            byte[] tag = new byte[_aesOptions.TagSize!.Value];

            using (var aesGcm = new AesGcm(Convert.FromBase64String(_aesOptions.Key!), tag.Length))
            {
                aesGcm.Encrypt(nonce, valueBytes, cipherText, tag);
            }

            byte[] encryptedValue = new byte[nonce.Length + cipherText.Length + tag.Length];
            Buffer.BlockCopy(nonce, 0, encryptedValue, 0, nonce.Length);
            Buffer.BlockCopy(cipherText, 0, encryptedValue, nonce.Length, cipherText.Length);
            Buffer.BlockCopy(tag, 0, encryptedValue, nonce.Length + cipherText.Length, tag.Length);

            return Convert.ToBase64String(encryptedValue);
        }

        public string DecryptValue(string encryptedValue)
        {
            if (string.IsNullOrEmpty(encryptedValue))
                throw new ArgumentException($"{nameof(encryptedValue)} cannot be null or empty");

            byte[] encryptedBytes = Convert.FromBase64String(encryptedValue);

            byte[] nonce = new byte[_aesOptions.NonceSize!.Value];
            byte[] tag = new byte[_aesOptions.TagSize!.Value];
            byte[] cipherText = new byte[encryptedBytes.Length - nonce.Length - tag.Length];

            Buffer.BlockCopy(encryptedBytes, 0, nonce, 0, nonce.Length);
            Buffer.BlockCopy(encryptedBytes, nonce.Length, cipherText, 0, cipherText.Length);
            Buffer.BlockCopy(encryptedBytes, nonce.Length + cipherText.Length, tag, 0, tag.Length);

            byte[] valueBytes = new byte[cipherText.Length];

            using (var aesGcm = new AesGcm(Convert.FromBase64String(_aesOptions.Key!), tag.Length))
            {
                aesGcm.Decrypt(nonce, cipherText, tag, valueBytes);
            }

            return Encoding.UTF8.GetString(valueBytes);
        }

        public bool CheckValue(string value, string encryptedValue)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(encryptedValue))
                return false;

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] decryptedValueBytes = Encoding.UTF8.GetBytes(DecryptValue(encryptedValue));
            return CryptographicOperations.FixedTimeEquals(valueBytes, decryptedValueBytes);
        }
    }
}
