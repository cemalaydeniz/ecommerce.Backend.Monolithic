using ecommerce.Infrastructure.Authentication;
using ecommerce.Test.Utility;
using ecommerce.Test.Utility.Fixtures.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ecommerce.Test.Unit.Infrastructure.Authentication
{
    public class JwtTokenHandlerTest : IClassFixture<JwtFixture>
    {
        private readonly JwtTokenHandler jwtTokenHandler;

        public JwtTokenHandlerTest(JwtFixture jwtFixture)
        {
            jwtTokenHandler = new JwtTokenHandler(Options.Create(jwtFixture.JwtOptions));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GenerateToken_WhenThereIsClaims_ReturnToken(bool newRefreshToken)
        {
            // Arrange
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, StringGenerator.Generate())
            };

            // Act
            var result = jwtTokenHandler.GenerateToken(claims, newRefreshToken);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);

            if (newRefreshToken)
            {
                Assert.NotNull(result.RefreshToken);
                Assert.NotNull(result.RefreshTokenExpirationDate);
            }
            else
            {
                Assert.Null(result.RefreshToken);
                Assert.Null(result.RefreshTokenExpirationDate);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GenerateToken_WhenThereIsNoClaims_ReturnToken(bool newRefreshToken)
        {
            // Act
            var result = jwtTokenHandler.GenerateToken(null, newRefreshToken);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);

            if (newRefreshToken)
            {
                Assert.NotNull(result.RefreshToken);
                Assert.NotNull(result.RefreshTokenExpirationDate);
            }
            else
            {
                Assert.Null(result.RefreshToken);
                Assert.Null(result.RefreshTokenExpirationDate);
            }
        }
    }
}
