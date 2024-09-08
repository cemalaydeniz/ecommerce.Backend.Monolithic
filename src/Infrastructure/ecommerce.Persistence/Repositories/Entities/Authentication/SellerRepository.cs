using ecommerce.Application.Repositories.Entities.Authentication;
using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Repositories.Entities.Authentication
{
    public class SellerRepository : BaseRepository<Seller, Guid>, ISellerRepository
    {
        public SellerRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(Seller newSeller)
        {
            await Table.AddAsync(newSeller);
        }

        public async Task<int> UpdateById(Guid sellerId, Expression<Func<SetPropertyCalls<Seller>, SetPropertyCalls<Seller>>> propertiesAndValues)
        {
            if (propertiesAndValues == null)
                return 0;

            int affectedRows = await Table
                .Where(u => u.Id.Equals(sellerId))
                .ExecuteUpdateAsync(propertiesAndValues);

            if (affectedRows == 0)
                return 0;

            Seller? user = Table.GetLoadedEntityByPrimaryKey(sellerId);
            if (user != null)
            {
                Table.Detach(user);
            }

            return affectedRows;
        }

        public void SoftDelete(Seller seller)
        {
            seller.Email = null;
            seller.PasswordHash = null;
            seller.PhoneNumber = null;
            seller.IsEmailConfirmed = false;
            seller.IsPhoneNumberConfirmed = false;
            seller.IsTwoFactorEnabled = false;
            seller.AccessFailCount = 0;
            seller.LockoutEndDate = null;
            seller.IsLockoutEnabled = false;
            seller.SecurityStamp = Guid.Empty;
            seller.BusinessName = null;
            seller.ContactName = null;
            seller.ContactEmail = null;
            seller.ContactPhoneNumber = null;
            seller.TinNumber = null;
            seller.VatNumber = null;
            seller.CreditCardInformation.CardHolderNameEncrypted = null;
            seller.CreditCardInformation.CardNumberEncrypted = null;
            seller.CreditCardInformation.ExpirationDateEncrypted = null;
            seller.CreditCardInformation.CvvCodeEncrypted = null;
            seller.AccountStatus = EAccountStatus.Suspended;

            Table.Remove(seller);
        }

        public async Task SoftDeleteById(Guid sellerId)
        {
            Seller? seller = await GetUserById(sellerId);
            if (seller == null)
                return;

            SoftDelete(seller);
        }

        public async Task<Seller?> GetUserById(Guid sellerId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(s => s.Id.Equals(sellerId))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetUserById<T>(Guid sellerId, Expression<Func<Seller, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            if (select == null)
                return null;

            return await Table
                .Where(s => s.Id.Equals(sellerId))
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Seller?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(s => s.Email != null && s.Email == email.ToLower())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T?> GetUserByEmail<T>(string email, Expression<Func<Seller, T>> select, CancellationToken cancellationToken = default)
            where T : class
        {
            if (select == null)
                return null;

            return await Table
                .Where(s => s.Email != null && s.Email == email.ToLower())
                .Select(select)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Address?> GetBusinessAddressById(Guid sellerId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(s => s.Id.Equals(sellerId))
                .Select(s => s.BusinessAddress)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Address?> GetBusinessAddressByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(s => s.Email != null && s.Email == email.ToLower())
                .Select(s => s.BusinessAddress)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Address?> GetBillingAddressById(Guid sellerId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(s => s.Id.Equals(sellerId))
                .Select(s => s.BillingAddress)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Address?> GetBillingAddressByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(s => s.Email != null && s.Email == email.ToLower())
                .Select(s => s.BillingAddress)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<SellerUploadedFile>> GetUploadedFilesById(Guid sellerId, CancellationToken cancellationToken = default)
        {
            return await Table
                .Where(s => s.Id.Equals(sellerId))
                .SelectMany(s => s.UploadedFiles)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<SellerUploadedFile>> GetUploadedFilesByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Table
               .Where(s => s.Email != null && s.Email == email.ToLower())
               .SelectMany(s => s.UploadedFiles)
               .ToListAsync(cancellationToken);
        }
    }
}
