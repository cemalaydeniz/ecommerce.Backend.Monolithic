using ecommerce.Infrastructure.Options.Authentication;

namespace ecommerce.Test.Utility.Fixtures.Options
{
    public class JwtFixture
    {
        public Jwt JwtOptions { get; set; }

        public JwtFixture()
        {
            JwtOptions = ConfigurationHelper.GetOption<Jwt>($"{nameof(Jwt)}")!;
        }
    }
}
