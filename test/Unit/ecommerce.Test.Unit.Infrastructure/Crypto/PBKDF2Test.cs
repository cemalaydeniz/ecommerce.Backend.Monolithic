using ecommerce.Infrastructure.Crypto;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Fixtures.Options;
using Microsoft.Extensions.Options;

namespace ecommerce.Test.Unit.Infrastructure.Crypto
{
    public class PBKDF2Test : IClassFixture<PBKDF2Fixture>
    {
        private readonly PBKDF2 pbkdf2;

        public PBKDF2Test(PBKDF2Fixture pbkdf2OptionsFixture)
        {
            pbkdf2 = new PBKDF2(Options.Create(pbkdf2OptionsFixture.pbkdf2Options));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashValue_WhenGivenValueIsNullOrEmpty_ThrowsException(string value)
        {
            // Act
            var e = Record.Exception(() => pbkdf2.HashValue(value));

            // Assert
            Assert.NotNull(e);
            Assert.IsType<ArgumentException>(e);
        }

        [Fact]
        public void HashValue_WhenGivenValueIsValid_HashesValue()
        {
            // Arrange
            string value = StringGenerator.Generate();

            // Act
            var result = pbkdf2.HashValue(value);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData("", "")]
        public void CheckValue_WhenGivenValuesAreNullOrEmpty_ThrowsException(string value, string hashedValue)
        {
            // Act
            var e = Record.Exception(() => pbkdf2.CheckValue(value, hashedValue));

            // Assert
            Assert.NotNull(e);
            Assert.IsType<ArgumentException>(e);
        }

        [Fact]
        public void CheckValue_WhenHashedVersionsofGivenValuesAreSame_ReturnsTrue()
        {
            // Act
            var value = StringGenerator.Generate();
            var hashedValue = pbkdf2.HashValue(value);

            // Act
            var result = pbkdf2.CheckValue(value, hashedValue);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckValue_WhenHashedVersionsofGivenValuesAreNotSame_ReturnsFalse()
        {
            // Act
            var value = StringGenerator.Generate();
            var hashedValue = pbkdf2.HashValue(StringGenerator.Generate());

            // Act
            var result = pbkdf2.CheckValue(value, hashedValue);

            // Assert
            Assert.False(result);
        }
    }
}
