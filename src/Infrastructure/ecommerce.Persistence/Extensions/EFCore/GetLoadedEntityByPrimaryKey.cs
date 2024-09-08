using ecommerce.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ecommerce.Persistence.Extensions.EFCore
{
    public static partial class EFCoreExtensions
    {
        /// <summary>
        /// Gets the entity in the <see cref="DbContext"/> that is already loaded from the database by its primary key. It does not hit the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to find</typeparam>
        /// <typeparam name="TKey">Type of the primary key of the entity to find</typeparam>
        /// <param name="primaryKey">Primary key value of the entity to find</param>
        /// <returns>Returns the entity if it is already in the memory, otherwise NULL</returns>
        public static TEntity? GetLoadedEntityByPrimaryKey<TEntity, TKey>(this DbSet<TEntity> table, TKey primaryKey)
            where TEntity : BaseEntity<TKey>
            where TKey : notnull
        {
            var dbContext = table.GetService<ICurrentDbContext>()?.Context;
            if (dbContext == null)
                return null;

            return dbContext.ChangeTracker.Entries<TEntity>()
                .Where(e => e.Entity.Id.Equals(primaryKey))
                .Select(e => e.Entity)
                .FirstOrDefault();
        }
    }
}
