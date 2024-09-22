using ecommerce.Domain.Repositories;
using ecommerce.Domain.SeedWork;
using ecommerce.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : notnull
    {
        private readonly AppDbContext _appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public DbSet<TEntity> Table => _appDbContext.Set<TEntity>();

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
