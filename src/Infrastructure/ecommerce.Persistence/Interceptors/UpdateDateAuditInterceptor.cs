using ecommerce.Domain.SeedWork;
using ecommerce.Persistence.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ecommerce.Persistence.Interceptors
{
    public class UpdateDateAuditInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ChangeUpdateDate(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            ChangeUpdateDate(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ChangeUpdateDate(DbContextEventData eventData)
        {
            if (eventData.Context == null)
                return;

            var entries = eventData.Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified)
                .ToList();

            if (entries.Count == 0)
                return;

            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                if (entity is IUpdateDateAudit)
                {
                    ReflectionHelper.SetValueofProperty(entity, nameof(IUpdateDateAudit.UpdatedDate), DateTime.UtcNow);
                }
                else
                {
                    entry.State = EntityState.Unchanged;
                }
            }
        }
    }
}
