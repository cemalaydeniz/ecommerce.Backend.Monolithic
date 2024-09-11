namespace ecommerce.Application.Abstractions.Crypto
{
    public interface IEncryptionOperations
    {
        /// <summary>
        /// Encryptes the given value
        /// </summary>
        /// <param name="value">Plain string to encrypt</param>
        /// <returns>Returns the encrypted string</returns>
        /// <exception cref="ArgumentException"></exception>
        string EncryptValue(string value);
        /// <summary>
        /// Decryptes the encrypted value
        /// </summary>
        /// <param name="encryptedValue">Encrypted string to decrypt</param>
        /// <returns>Returns the plain string</returns>
        /// <exception cref="ArgumentException"></exception>
        string DecryptValue(string encryptedValue);
        /// <summary>
        /// Checks if the given value is the same as the decrypted version of the encrypted value
        /// </summary>
        /// <param name="value">Plain string to compare</param>
        /// <param name="encryptedValue">Encrypted string to decrypt and compare</param>
        /// <returns>Returns TRUE if the values are matched, otherwise FALSE</returns>
        bool CheckValue(string value, string encryptedValue);
    }
}
