using ecommerce.Application.Abstractions.Crypto;
using ecommerce.Infrastructure.Options.Crypto;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace ecommerce.Infrastructure.Crypto
{
    /// <summary>
    /// FIPS compliance hashing algorithm
    /// </summary>
    public class PBKDF2 : IHashOperations
    {
        private readonly CryptoOptions.PBKDF2 _pbkdf2Options;

        public PBKDF2(IOptions<CryptoOptions.PBKDF2> pbkdf2Options)
        {
            _pbkdf2Options = pbkdf2Options.Value;
        }

        public string HashValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{nameof(value)} cannot be null or empty");

            byte[] salt;
            using (var rng = RandomNumberGenerator.Create())
            {
                salt = new byte[_pbkdf2Options.SaltSize!.Value];
                rng.GetBytes(salt);
            }

            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(value, salt, _pbkdf2Options.Iterations!.Value, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(_pbkdf2Options.KeySize!.Value);
            }

            byte[] hashBytes = new byte[_pbkdf2Options.SaltSize!.Value + _pbkdf2Options.KeySize!.Value];
            Array.Copy(salt, 0, hashBytes, 0, _pbkdf2Options.SaltSize!.Value);
            Array.Copy(hash, 0, hashBytes, _pbkdf2Options.SaltSize!.Value, _pbkdf2Options.KeySize!.Value);

            return Convert.ToBase64String(hashBytes);
        }

        public bool CheckValue(string value, string hashedValue)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{nameof(value)} cannot be null or empty");
            if (string.IsNullOrEmpty(hashedValue))
                throw new ArgumentException($"{nameof(hashedValue)} cannot be null or empty");

            byte[] hashedValueBytes = Convert.FromBase64String(hashedValue);
            byte[] salt = new byte[_pbkdf2Options.SaltSize!.Value];
            Array.Copy(hashedValueBytes, 0, salt, 0, _pbkdf2Options.SaltSize!.Value);

            byte[] storedHash = new byte[_pbkdf2Options.KeySize!.Value];
            Array.Copy(hashedValueBytes, _pbkdf2Options.SaltSize!.Value, storedHash, 0, _pbkdf2Options.KeySize!.Value);

            byte[] computedHash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(value, salt, _pbkdf2Options.Iterations!.Value, HashAlgorithmName.SHA256))
            {
                computedHash = pbkdf2.GetBytes(_pbkdf2Options.KeySize!.Value);
            }

            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
    }
}
