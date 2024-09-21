namespace ecommerce.Infrastructure.Options.Authentication
{
    public class Jwt
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int? AccessTokenLifeSpanInMinutes { get; set; }
        public int? RefreshTokenLifeSpanInMinutes { get; set; }
    }
}
