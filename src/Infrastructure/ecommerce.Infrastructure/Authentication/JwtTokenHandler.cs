using ecommerce.Application.Abstractions.Authentication;
using ecommerce.Application.Dtos.Authentication;
using ecommerce.Infrastructure.Options.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ecommerce.Infrastructure.Authentication
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly Jwt _jwtOptions;

        public JwtTokenHandler(IOptions<Jwt> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public JwtTokenDto GenerateToken(IEnumerable<Claim>? claims = null, bool newRefreshToken = true)
        {
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey!));

            var securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifeSpanInMinutes!.Value),
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtTokenDto()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };

            if (newRefreshToken)
            {
                token.RefreshToken = GenerateRefreshToken();
                token.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenLifeSpanInMinutes!.Value);
            }

            return token;
        }

        private string GenerateRefreshToken()
        {
            byte[] numbers = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            return Convert.ToBase64String(numbers);
        }
    }
}
