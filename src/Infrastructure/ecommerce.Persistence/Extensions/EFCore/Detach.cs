using ecommerce.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ecommerce.Persistence.Extensions.EFCore
{
    public static partial class EFCoreExtensions
    {
        public static void Detach<TEntity>(this DbSet<TEntity> table, TEntity? entity)
            where TEntity : class
        {
            if (entity == null)
                return;

            var dbContext = table.GetService<ICurrentDbContext>()?.Context;
            if (dbContext == null)
            {
                entity = null;
                return;
            }

            dbContext.Entry(entity).State = EntityState.Detached;
            entity = null;
        }
    }
}
