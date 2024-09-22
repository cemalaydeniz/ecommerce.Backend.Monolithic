using ecommerce.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Domain.Repositories.Entities.Account
{
    public interface IAddressRepository : IBaseRepository<Address, Guid>
    {
        /// <summary>
        /// Updates specific properties of the login by its ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="addressId">ID of the address to update</param>
        /// <param name="propertiesAndValues">Expression to update properties</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> UpdateById(Guid addressId, Expression<Func<SetPropertyCalls<Address>, SetPropertyCalls<Address>>> propertiesAndValues);

        /// <summary>
        /// Deletes the address from the table
        /// </summary>
        /// <param name="address"></param>
        void Delete(Address address);
        /// <summary>
        /// Deletes the address from the table by its ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="addressId">ID of the address to delete</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteById(Guid addressId);
        /// <summary>
        /// Deletes the addresses from the table
        /// </summary>
        /// <param name="addresses">Addresses to delete</param>
        void DeleteMultiple(IEnumerable<Address> addresses);
        /// <summary>
        /// Delete all addresses by their IDs immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="ids">IDs of the addresses to delete</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteAllById(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    }
}
