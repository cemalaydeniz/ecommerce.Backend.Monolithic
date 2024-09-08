#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Options;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Test.Utility.Fixtures
{
    public class AppDbContextFixture : IAsyncLifetime
    {
        public AppDbContext AppDbContext { get; private set; }

        public async Task InitializeAsync()
        {
            var connectionStrings = ConfigurationsHelper.GetOption<ConnectionStrings>(nameof(ConnectionStrings));

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionStrings!.AppTest, options =>
                {
                    options.MigrationsAssembly("ecommerce.Persistence");
                })
                .Options;

            AppDbContext = new AppDbContext(options);
            await AppDbContext.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await AppDbContext.Database.EnsureDeletedAsync();
            await AppDbContext.DisposeAsync();
        }
    }
}
