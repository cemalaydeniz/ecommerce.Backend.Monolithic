using ecommerce.Domain.SeedWork;
using ecommerce.Persistence.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ecommerce.Persistence.Interceptors
{
    public class CreatedDateInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            AddCreatedDate(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            AddCreatedDate(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddCreatedDate(DbContextEventData eventData)
        {
            if (eventData.Context == null)
                return;

            var entities = eventData.Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList();

            if (entities.Count == 0)
                return;

            foreach (var entity in entities)
            {
                ReflectionHelper.AssignValueToPrivateSetterProperty(entity, nameof(BaseEntity<Guid>.CreatedDate), DateTime.UtcNow);
            }
        }
    }
}
