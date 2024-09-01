using ecommerce.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Filters
{
    public static class SoftDeleteFilter
    {
        public static void AddGlobalSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var type in entityTypes)
            {
                var clrType = type.ClrType;
                if (typeof(ISoftDelete).IsAssignableFrom(clrType) && type.BaseType == null)
                {
                    var parameter = Expression.Parameter(clrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                    var value = Expression.Constant(false);
                    var filter = Expression.Lambda(Expression.Equal(property, value), parameter);

                    modelBuilder.Entity(clrType).HasQueryFilter(filter);
                }
            }
        }
    }
}
