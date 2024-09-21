using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Persistence
{
    public static class ServiceRegistrations
    {
        public static void AddPersistenceServices(this IServiceCollection services, ConnectionStrings? connectionStrings)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionStrings!.App));
        }
    }
}
