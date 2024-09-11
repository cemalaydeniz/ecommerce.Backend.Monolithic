namespace ecommerce.Application.Abstractions.Crypto
{
    public interface IHashOperations
    {
        /// <summary>
        /// Hashes the given value
        /// </summary>
        /// <param name="value">Plain string to hash</param>
        /// <returns>Returns the hashed string</returns>
        /// <exception cref="ArgumentException"></exception>
        string HashValue(string value);
        /// <summary>
        /// Checks if the hashed version of the given value is the same as the hashed value
        /// </summary>
        /// <param name="value">Plain string to hash and compare</param>
        /// <param name="valueHash">Hashed string to compare</param>
        /// <returns>Returns TRUE if the hashed values are matched, otherwise FALSE</returns>
        /// <exception cref="ArgumentException"></exception>
        bool CheckValue(string value, string valueHash);
    }
}
