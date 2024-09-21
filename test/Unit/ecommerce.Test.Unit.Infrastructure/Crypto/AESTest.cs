using ecommerce.Infrastructure.Crypto;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Fixtures.Options;
using Microsoft.Extensions.Options;

namespace ecommerce.Test.Unit.Infrastructure.Crypto
{
    public class AESTest : IClassFixture<AESFixture>
    {
        private readonly AES aes;

        public AESTest(AESFixture aesFixture)
        {
            aes = new AES(Options.Create(aesFixture.AesOptions));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void EncryptValue_WhenGivenValueIsNullOrEmpty_ThrowsException(string value)
        {
            // Act
            var e = Record.Exception(() => aes.EncryptValue(value));

            // Assert
            Assert.NotNull(e);
            Assert.IsType<ArgumentException>(e);
        }

        [Fact]
        public void EncryptValue_WhenGivenValueIsValid_EncryptsValue()
        {
            // Arrange
            string value = StringGenerator.Generate();

            // Act
            var result = aes.EncryptValue(value);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void DecryptValue_WhenGivenValueIsNullOrEmpty_ThrowsException(string encryptedValue)
        {
            // Act
            var e = Record.Exception(() => aes.DecryptValue(encryptedValue));

            // Assert
            Assert.NotNull(e);
            Assert.IsType<ArgumentException>(e);
        }

        [Fact]
        public void DecryptValue_WhenGivenValueIsValid_DecryptsValue()
        {
            // Arrange
            string value = StringGenerator.Generate();
            string encryptedValue = aes.EncryptValue(value);

            // Act
            var result = aes.DecryptValue(encryptedValue);

            // Assert
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData("", "")]
        public void CheckValue_WhenGivenValuesAreNullOrEmpty_ReturnsFalse(string value, string encryptedValue)
        {
            // Act
            var result = aes.CheckValue(value, encryptedValue);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckValue_WhenDecrpytedVersionsofGivenValuesAreSame_ReturnsTrue()
        {
            // Arrange
            string value = StringGenerator.Generate();
            string encryptedValue = aes.EncryptValue(value);

            // Act
            var result = aes.CheckValue(value, encryptedValue);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckValue_WhenDecrpytedVersionsofGivenValuesAreNotSame_ReturnsTrue()
        {
            // Arrange
            string value = StringGenerator.Generate();
            string encryptedValue = aes.EncryptValue(StringGenerator.Generate());

            // Act
            var result = aes.CheckValue(value, encryptedValue);

            // Assert
            Assert.False(result);
        }
    }
}
