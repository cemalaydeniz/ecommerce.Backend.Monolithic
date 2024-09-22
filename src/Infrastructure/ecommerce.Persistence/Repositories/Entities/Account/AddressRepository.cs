using ecommerce.Domain.Repositories.Entities.Account;
using ecommerce.Domain.Entities.Account;
using ecommerce.Persistence.DbContexts;
using ecommerce.Persistence.Extensions.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Persistence.Repositories.Entities.Account
{
    public class AddressRepository : BaseRepository<Address, Guid>, IAddressRepository
    {
        public AddressRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<int> UpdateById(Guid addressId, Expression<Func<SetPropertyCalls<Address>, SetPropertyCalls<Address>>> propertiesAndValues)
        {
            if (propertiesAndValues == null)
                return 0;

            int affectedRows = await Table
                .Where(a => a.Id.Equals(addressId))
                .ExecuteUpdateAsync(propertiesAndValues);

            if (affectedRows == 0)
                return 0;

            Address? updatedAddress = Table.GetLoadedEntityByPrimaryKey(addressId);
            if (updatedAddress != null)
            {
                Table.Detach(updatedAddress);
            }

            return affectedRows;
        }

        public void Delete(Address address)
        {
            Table.Remove(address);
        }

        public async Task<int> DeleteById(Guid addressId)
        {
            int affectedRows = await Table
                .Where(a => a.Id.Equals(addressId))
                .ExecuteDeleteAsync();

            if (affectedRows == 0)
                return 0;

            Address? deletedAddress = Table.GetLoadedEntityByPrimaryKey(addressId);
            if (deletedAddress != null)
            {
                Table.Detach(deletedAddress);
            }

            return affectedRows;
        }

        public void DeleteMultiple(IEnumerable<Address> addresses)
        {
            Table.RemoveRange(addresses);
        }

        public async Task<int> DeleteAllById(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            int affectedRows = await Table
                .Where(a => ids.Contains(a.Id))
                .ExecuteDeleteAsync(cancellationToken);

            if (affectedRows == 0)
                return 0;

            foreach (var id in ids)
            {
                Address? deletedAddress = Table.GetLoadedEntityByPrimaryKey(id);
                if (deletedAddress != null)
                {
                    Table.Detach(deletedAddress);
                }
            }

            return affectedRows;
        }
    }
}
