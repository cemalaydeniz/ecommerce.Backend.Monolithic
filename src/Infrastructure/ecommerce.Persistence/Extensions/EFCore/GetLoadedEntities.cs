using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Extensions.EFCore
{
    public static partial class EFCoreExtensions
    {
        /// <summary>
        /// Gets the entities in the <see cref="DbContext"/> that are already loaded from the database. It does not hit the database
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to find</typeparam>
        /// <param name="search">Expression to search entities</param>
        /// <returns>Returns the entities if they are already in the memory, otherwise an empty list</returns>
        public static IEnumerable<TEntity> GetLoadedEntities<TEntity>(this DbSet<TEntity> table, Expression<Func<TEntity, bool>> search)
            where TEntity : class
        {
            var dbContext = table.GetService<ICurrentDbContext>()?.Context;
            if (dbContext == null)
                return Enumerable.Empty<TEntity>();

            var func = search.Compile();
            return dbContext.ChangeTracker.Entries<TEntity>()
                .Where(e => func.Invoke(e.Entity))
                .Select(e => e.Entity);
        }
    }
}
