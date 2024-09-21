namespace ecommerce.Application.Dtos.Authentication
{
    public class JwtTokenDto
    {
        public string AccessToken { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
    }
}
