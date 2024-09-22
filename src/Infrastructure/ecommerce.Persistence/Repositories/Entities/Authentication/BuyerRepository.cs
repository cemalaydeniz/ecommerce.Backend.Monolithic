using ecommerce.Domain.Repositories.Entities.Authentication;
using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Repositories.Entities.Authentication
{
    public class BuyerRepository : BaseRepository<Buyer, Guid>, IBuyerRepository
    {
        public BuyerRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(Buyer newBuyer)
        {
            await Table.AddAsync(newBuyer);
        }

        public async Task<int> UpdateById(Guid buyerId, Expression<Func<SetPropertyCalls<Buyer>, SetPropertyCalls<Buyer>>> propertiesAndValues)
        {
            if (propertiesAndValues == null)
                return 0;

            int affectedRows = await Table
                .Where(u => u.Id.Equals(buyerId))
                .ExecuteUpdateAsync(propertiesAndValues);

            if (affectedRows == 0)
                return 0;

            Buyer? user = Table.GetLoadedEntityByPrimaryKey(buyerId);
            if (user != null)
            {
                Table.Detach(user);
            }

            return affectedRows;
        }

        public void SoftDelete(Buyer buyer)
        {
            buyer.Email = null;
            buyer.PasswordHash = null;
            buyer.PhoneNumber = null;
            buyer.IsEmailConfirmed = false;
            buyer.IsPhoneNumberConfirmed = false;
            buyer.IsTwoFactorEnabled = false;
            buyer.AccessFailCount = 0;
            buyer.LockoutEndDate = null;
            buyer.IsLockoutEnabled = false;
            buyer.SecurityStamp = Guid.Empty;
            buyer.FullName = null;

            Table.Remove(buyer);
        }

        public async Task SoftDeleteById(Guid buyerId)
        {
            Buyer? buyer = await GetUserById(buyerId);
            if (buyer == null)
                return;

            SoftDelete(buyer);
        }

        public async Task<Buyer?> GetUserById(Guid buyerId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(b => b.Id.Equals(buyerId))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetUserById<T>(Guid buyerId, Expression<Func<Buyer, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            if (select == null)
                return null;

            return await Table
                .Where(b => b.Id.Equals(buyerId))
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Buyer?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(b => b.Email != null && b.Email == email.ToLower())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetUserByEmail<T>(string email, Expression<Func<Buyer, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            if (select == null)
                return null;

            return await Table
                .Where(b => b.Email != null && b.Email == email.ToLower())
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAllAddressesById(Guid buyerId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(b => b.Id.Equals(buyerId))
                .Include(b => b.Addresses)
                .SelectMany(b => b.Addresses)
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetAllAddressesByEmail(string email, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(b => b.Email != null && b.Email == email.ToLower())
                .Include(b => b.Addresses)
                .SelectMany(b => b.Addresses)
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
