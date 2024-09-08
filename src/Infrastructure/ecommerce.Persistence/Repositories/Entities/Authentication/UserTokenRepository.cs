using ecommerce.Application.Repositories.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistence.Repositories.Entities.Authentication
{
    public class UserTokenRepository : BaseRepository<UserToken, Guid>, IUserTokenRepository
    {
        public UserTokenRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(UserToken newToken)
        {
            await Table.AddAsync(newToken);
        }

        public void Delete(UserToken token)
        {
            Table.Entry(token).State = EntityState.Deleted;
        }

        public async Task<int> DeleteById(Guid tokenId)
        {
            int affectedRows = await Table
                .Where(t => t.Id.Equals(tokenId))
                .ExecuteDeleteAsync();

            if (affectedRows == 0)
                return 0;

            UserToken? deletedToken = Table.GetLoadedEntityByPrimaryKey(tokenId);
            if (deletedToken != null)
            {
                Table.Detach(deletedToken);
            }

            return affectedRows;
        }

        public async Task<int> DeleteAllByUserId(Guid userId)
        {
            int affectedRows = await Table
                .Where(t => t.UserId.Equals(userId))
                .ExecuteDeleteAsync();

            if (affectedRows == 0)
                return 0;

            var deletedTokens = Table.GetLoadedEntities(t => t.UserId.Equals(userId));
            foreach (var token in deletedTokens)
            {
                Table.Detach(token);
            }

            return affectedRows;
        }

        public async Task<UserToken?> GetTokenById(Guid tokenId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(t => t.Id.Equals(tokenId))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserToken>> GetTokensByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(t => t.UserId.Equals(userId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserToken>> GetTokensByUserIdAndTokenPurpose(Guid userId, ETokenPurpose tokenPurpose, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(t => t.UserId.Equals(userId) &&
                    t.Purpose == tokenPurpose)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserToken>> GetAllExpiredTokensByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(t => t.UserId.Equals(userId) &&
                    t.Token.ExpirationDate < DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }
    }
}
