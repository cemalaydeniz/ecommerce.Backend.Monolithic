using ecommerce.Test.Utility.Fixtures;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace ecommerce.Test.Utility
{
    public static class ConfigurationHelper
    {
        private static IConfiguration? _configuration = null;
        private static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Path.Combine(GetThisFilePath()!, "../../../src/Presentation/ecommerce.API"))
                        .AddJsonFile("appsettings.json", false, true)
                        .AddUserSecrets<AppDbContextFixture>();

                    _configuration = builder.Build();
                }

                return _configuration;
            }
        }

        private static string? GetThisFilePath([CallerFilePath] string? path = null) => path;

        public static T? GetOption<T>(string sectionName)
        {
            return Configuration.GetSection(sectionName).Get<T>();
        }
    }
}
