using ecommerce.Domain.Entities.Account;
using ecommerce.Domain.Entities.Authentication;

namespace ecommerce.Domain.Repositories.Entities.Authentication
{
    public interface ISellerRepository : IUserRepository<Seller>
    {
        /// <summary>
        /// Gets the business address of the seller by the seller's ID
        /// </summary>
        /// <param name="sellerId">ID of the seller to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the business address of the seller if it is found, otherwise NULL</returns>
        Task<Address?> GetBusinessAddressById(Guid sellerId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the business address of the seller by the seller's email
        /// </summary>
        /// <param name="email">Email of the seller to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the business address of the seller if it is found, otherwise NULL</returns>
        Task<Address?> GetBusinessAddressByEmail(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the billing address of the seller by the seller's ID
        /// </summary>
        /// <param name="sellerId">ID of the seller to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the billing address of the seller if it is found, otherwise NULL</returns>
        Task<Address?> GetBillingAddressById(Guid sellerId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the billing address of the seller by the seller's email
        /// </summary>
        /// <param name="email">Email of the seller to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the billing address of the seller if it is found, otherwise NULL</returns>
        Task<Address?> GetBillingAddressByEmail(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all uploaded files by the seller by the seller's ID
        /// </summary>
        /// <param name="sellerId">ID of the seller to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the uploaded files by the seller if there is any, otherwise an empty list</returns>
        Task<IEnumerable<SellerUploadedFile>> GetUploadedFilesById(Guid sellerId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all uploaded files by the seller by the seller's email
        /// </summary>
        /// <param name="email">Email of the seller to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the uploaded files by the seller if there is any, otherwise an empty list</returns>
        Task<IEnumerable<SellerUploadedFile>> GetUploadedFilesByEmail(string email, CancellationToken cancellationToken = default);
    }
}
