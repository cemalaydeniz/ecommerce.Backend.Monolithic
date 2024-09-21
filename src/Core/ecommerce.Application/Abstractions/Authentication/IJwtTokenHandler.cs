using ecommerce.Application.Dtos.Authentication;
using System.Security.Claims;

namespace ecommerce.Application.Abstractions.Authentication
{
    public interface IJwtTokenHandler
    {
        JwtTokenDto GenerateToken(IEnumerable<Claim>? claims = null, bool newRefreshToken = true);
    }
}
