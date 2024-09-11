using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Exceptions;
using ecommerce.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Persistence
{
    public static class ServiceRegistrations
    {
        public static void AddPersistenceServices(this IServiceCollection services, ConnectionStrings? connectionStrings)
        {
            CheckConnectionStrings(connectionStrings);

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionStrings!.App));
        }

        private static void CheckConnectionStrings(ConnectionStrings? connectionStrings)
        {
            if (connectionStrings == null ||
                connectionStrings.App == null ||
                connectionStrings.AppTest == null)
                throw new ConfigurationException($"{nameof(ConnectionStrings)} configuration is not found or is not set properly");
        }
    }
}
