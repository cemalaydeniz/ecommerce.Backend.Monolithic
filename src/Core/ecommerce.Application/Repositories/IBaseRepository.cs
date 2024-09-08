using ecommerce.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Application.Repositories
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : notnull
    {
        /// <summary>
        /// The DbSet of the entity
        /// </summary>
        DbSet<TEntity> Table { get; }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> SaveChangesAsync();
    }
}
