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
            if (connectionStrings == null)
                throw new ConfigurationException("Connection string configuration is not found");

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionStrings.App));
        }
    }
}
