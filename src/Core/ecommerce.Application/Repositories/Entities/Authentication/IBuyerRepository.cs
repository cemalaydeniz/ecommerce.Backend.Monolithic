using ecommerce.Application.Models.Common;
using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication;

namespace ecommerce.Application.Repositories.Entities.Authentication
{
    public interface IBuyerRepository : IUserRepository<Buyer>
    {
        /// <summary>
        /// Gets all addresses of the buyer by the buyer's ID
        /// </summary>
        /// <param name="buyerId">ID of the buyer to find</param>
        /// <param name="pagination">Limitation of the returned data</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the addresses of the buyer if there is any, otherwise an empty list</returns>
        Task<IEnumerable<Address>> GetAllAddressesById(Guid buyerId, Pagination pagination, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all addresses of the buyer by the buyer's email
        /// </summary>
        /// <param name="email">Email of the buyer to find</param>
        /// <param name="pagination">Limitation of the returned data</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the addresses of the buyer if there is any, otherwise an empty list</returns>
        Task<IEnumerable<Address>> GetAllAddressesByEmail(string email, Pagination pagination, CancellationToken cancellationToken = default);
    }
}
