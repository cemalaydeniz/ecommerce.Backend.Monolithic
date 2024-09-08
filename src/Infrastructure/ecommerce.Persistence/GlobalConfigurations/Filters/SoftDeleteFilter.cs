using ecommerce.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce.Persistence.GlobalConfigurations.Filters
{
    public static class SoftDeleteFilter
    {
        public static void AddGlobalSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            var entities = modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(ISoftDelete).IsAssignableFrom(e.ClrType))
                .ToList();

            foreach (var entity in entities)
            {
                if (entity.BaseType != null)
                    continue;

                var parameter = Expression.Parameter(entity.ClrType, "e");
                var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);

                entity.SetQueryFilter(filter);
            }
        }
    }
}
