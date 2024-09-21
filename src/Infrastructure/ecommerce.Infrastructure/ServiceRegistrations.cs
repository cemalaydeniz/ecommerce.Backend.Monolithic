using ecommerce.Application.Abstractions.Authentication;
using ecommerce.Application.Abstractions.Crypto;
using ecommerce.Infrastructure.Crypto;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Infrastructure
{
    public static class ServiceRegistrations
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IHashOperations, PBKDF2>();
            services.AddSingleton<IEncryptionOperations, AES>();

            services.AddSingleton<IJwtTokenHandler, IJwtTokenHandler>();
        }
    }
}
