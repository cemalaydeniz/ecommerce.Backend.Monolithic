using ecommerce.Domain.SeedWork;
using ecommerce.Persistence.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ecommerce.Persistence.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ConvertDeleteToSoftDelete(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken)
        {
            ConvertDeleteToSoftDelete(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ConvertDeleteToSoftDelete(DbContextEventData eventData)
        {
            if (eventData.Context == null)
                return;

            var entries = eventData.Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted)
                .ToList();

            if (entries.Count == 0)
                return;

            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                if (entity is ISoftDelete)
                {
                    ReflectionHelper.SetValueofProperty(entity, nameof(ISoftDelete.IsDeleted), true);
                    ReflectionHelper.SetValueofProperty(entity, nameof(ISoftDelete.SoftDeletedDate), DateTime.UtcNow);

                    eventData.Context.Entry(entity).State = EntityState.Modified;
                }
            }
        }
    }
}
