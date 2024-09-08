using ecommerce.Application.Repositories.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Repositories.Entities.Authentication
{
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository<User>
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(User newUser)
        {
            await Table.AddAsync(newUser);
        }

        public async Task<int> UpdateById(Guid userId, Expression<Func<SetPropertyCalls<User>, SetPropertyCalls<User>>> propertiesAndValues)
        {
            if (propertiesAndValues == null)
                return 0;

            int affectedRows = await Table
                .Where(u => u.Id.Equals(userId))
                .ExecuteUpdateAsync(propertiesAndValues);

            if (affectedRows == 0)
                return 0;

            User? user = Table.GetLoadedEntityByPrimaryKey(userId);
            if (user != null)
            {
                Table.Detach(user);
            }

            return affectedRows;
        }

        public void SoftDelete(User user)
        {
            user.Email = null;
            user.PasswordHash = null;
            user.PhoneNumber = null;
            user.IsEmailConfirmed = false;
            user.IsPhoneNumberConfirmed = false;
            user.IsTwoFactorEnabled = false;
            user.AccessFailCount = 0;
            user.LockoutEndDate = null;
            user.IsLockoutEnabled = false;
            user.SecurityStamp = Guid.Empty;

            Table.Remove(user);
        }

        public async Task SoftDeleteById(Guid userId)
        {
            User? user = await GetUserById(userId);
            if (user == null)
                return;

            SoftDelete(user);
        }

        public async Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(u => u.Id.Equals(userId))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetUserById<T>(Guid userId, Expression<Func<User, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            if (select == null)
                return null;

            return await Table
                .Where(u => u.Id.Equals(userId))
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(u => u.Email != null && u.Email == email.ToLower())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetUserByEmail<T>(string email, Expression<Func<User, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            if (select == null)
                return null;

            return await Table
                .Where(u => u.Email != null && u.Email == email.ToLower())
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
