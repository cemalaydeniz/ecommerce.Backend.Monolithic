using ecommerce.Application.Models.Common;
using ecommerce.Application.Repositories.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Repositories.Entities.Authentication
{
    public class UserRoleRepository : BaseRepository<UserRole, Guid>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(UserRole newRole)
        {
            await Table.AddAsync(newRole);
        }

        public void SoftDelete(UserRole role)
        {
            Table.Remove(role);
        }

        public async Task SoftDeleteById(Guid roleId)
        {
            UserRole? role = await GetRoleById(roleId);
            if (role == null)
                return;

            Table.Remove(role);
        }

        public async Task<UserRole?> GetRoleById(Guid roleId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(r => r.Id.Equals(roleId))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<UserRole?> GetRoleByName(string roleName, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserRole>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(r => r.Users.Any(u => u.Id == userId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleId<T>(Guid roleId, Pagination pagination, Expression<Func<User, T>> orderBy, CancellationToken cancellationToken = default)
            where T : class
        {
            return await Table
                .Where(r => r.Id.Equals(roleId))
                .SelectMany(r => r.Users)
                .OrderBy(orderBy)
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T1>> GetUsersByRoleId<T1, T2>(Guid roleId, Expression<Func<User, T1>> select, Pagination pagination, Expression<Func<T1, T2>> orderBy, CancellationToken cancellationToken = default)
            where T1 : class
            where T2 : class
        {
            return await Table
                .Where(r => r.Id.Equals(roleId))
                .SelectMany(r => r.Users)
                .Select(select)
                .OrderBy(orderBy)
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleName<T>(string roleName, Pagination pagination, Expression<Func<User, T>> orderBy, CancellationToken cancellationToken = default)
            where T : class
        {
            return await Table
                .Where(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                .SelectMany(r => r.Users)
                .OrderBy(orderBy)
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T1>> GetUsersByRoleName<T1, T2>(string roleName, Expression<Func<User, T1>> select, Pagination pagination, Expression<Func<T1, T2>> orderBy, CancellationToken cancellationToken = default)
            where T1 : class
            where T2 : class
        {
            return await Table
                .Where(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                .SelectMany(r => r.Users)
                .Select(select)
                .OrderBy(orderBy)
                .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
