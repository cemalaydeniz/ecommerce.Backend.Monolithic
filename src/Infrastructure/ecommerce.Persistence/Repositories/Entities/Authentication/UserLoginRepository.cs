using ecommerce.Domain.Repositories.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Repositories.Entities.Authentication
{
    public class UserLoginRepository : BaseRepository<UserLogin, Guid>, IUserLoginRepository
    {
        public UserLoginRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(UserLogin newLogin)
        {
            await Table.AddAsync(newLogin);
        }
        
        public async Task<int> UpdateById(Guid loginId, Expression<Func<SetPropertyCalls<UserLogin>, SetPropertyCalls<UserLogin>>> propertiesAndValues)
        {
            if (propertiesAndValues == null)
                return 0;

            int affectedRows = await Table
                .Where(l => l.Id.Equals(loginId))
                .ExecuteUpdateAsync(propertiesAndValues);

            if (affectedRows == 0)
                return 0;

            UserLogin? updatedLogin = Table.GetLoadedEntityByPrimaryKey(loginId);
            if (updatedLogin != null)
            {
                Table.Detach(updatedLogin);
            }

            return affectedRows;
        }

        public void Delete(UserLogin login)
        {
            Table.Remove(login);
        }

        public async Task<int> DeleteById(Guid loginId)
        {
            int affectedRows =  await Table
                .Where(l => l.Id.Equals(loginId))
                .ExecuteDeleteAsync();

            if (affectedRows == 0)
                return 0;

            UserLogin? deletedLogin = Table.GetLoadedEntityByPrimaryKey(loginId);
            if (deletedLogin != null)
            {
                Table.Detach(deletedLogin);
            }

            return affectedRows;
        }

        public async Task<int> DeleteAllByUserId(Guid userId)
        {
            int affectedRows = await Table
                .Where(l => l.UserId.Equals(userId))
                .ExecuteDeleteAsync();

            if (affectedRows == 0)
                return 0;

            var deletedLogins = Table.GetLoadedEntities(l => l.UserId.Equals(userId));
            foreach (var login in deletedLogins)
            {
                Table.Detach(login);
            }

            return affectedRows;
        }

        public async Task<UserLogin?> GetLoginById(Guid loginId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(l => l.Id.Equals(loginId))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetLoginById<T>(Guid loginId, Expression<Func<UserLogin, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            return await Table
                .Where(l => l.Id.Equals(loginId))
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserLogin>> GetLoginsByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(l => l.UserId.Equals(userId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetLoginsByUserId<T>(Guid userId, Expression<Func<UserLogin, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            return await Table
                .Where(l => l.UserId.Equals(userId))
                .Select(select)
                .ToListAsync(cancellationToken);
        }
    }
}
