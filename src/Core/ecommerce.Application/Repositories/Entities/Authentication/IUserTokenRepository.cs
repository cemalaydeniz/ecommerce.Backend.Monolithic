using ecommerce.Domain.Entities.Authentication;
using ecommerce.Domain.Entities.Authentication.Enums;

namespace ecommerce.Application.Repositories.Entities.Authentication
{
    public interface IUserTokenRepository : IBaseRepository<UserToken, Guid>
    {
        /// <summary>
        /// Adds a new token to the table
        /// </summary>
        /// <param name="newToken">New tokent to add</param>
        Task AddAsync(UserToken newToken);

        /// <summary>
        /// Deletes the token from the table by its ID
        /// </summary>
        /// <param name="token">Token to delete</param>
        void Delete(UserToken token);
        /// <summary>
        /// Deletes the token from the table by its ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="tokenId">ID of the token to delete</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteById(Guid tokenId);
        ///// <summary>
        ///// Deletes the tokens from the table by their IDs immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        ///// </summary>
        ///// <param name="ids">IDs of the tokens to delete</param>
        ///// <param name="cancellationToken">Token to check for the cancellation request</param>
        ///// <returns>Returns the number of affected rows</returns>
        //Task<int> DeleteAllById(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        /// <summary>
        /// Deletes all tokens related to the user by the user's ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteAllByUserId(Guid userId);

        /// <summary>
        /// Gets the token by its ID
        /// </summary>
        /// <param name="tokenId">ID of the token to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the token if it is found, otherwise NULL</returns>
        Task<UserToken?> GetTokenById(Guid tokenId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all tokens related to the user by the user's ID
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the tokens ordered by their expiration dates if there is any, otherwise an empty list</returns>
        Task<IEnumerable<UserToken>> GetTokensByUserId(Guid userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all tokens related to the user by the user's ID and the purpose of the tokens
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <param name="tokenPurpose">Purpose of the tokens</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the tokens ordered by their expiration dates if there is any, otherwise an empty list</returns>
        Task<IEnumerable<UserToken>> GetTokensByUserIdAndTokenPurpose(Guid userId, ETokenPurpose tokenPurpose, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all expired tokens related to the user by the user's ID
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the expired tokens if there is any, otherwise an empty list</returns>
        Task<IEnumerable<UserToken>> GetAllExpiredTokensByUserId(Guid userId, CancellationToken cancellationToken = default);
    }
}
