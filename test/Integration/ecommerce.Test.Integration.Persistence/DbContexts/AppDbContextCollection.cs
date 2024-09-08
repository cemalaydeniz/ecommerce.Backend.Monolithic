using ecommerce.Test.Utility.Fixtures;

namespace ecommerce.Test.Integration.Persistence.DbContexts
{
    [CollectionDefinition(nameof(AppDbContextCollection))]
    public class AppDbContextCollection : ICollectionFixture<AppDbContextFixture>
    {
    }
}
