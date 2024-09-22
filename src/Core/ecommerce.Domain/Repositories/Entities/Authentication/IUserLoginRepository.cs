using ecommerce.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Domain.Repositories.Entities.Authentication
{
    public interface IUserLoginRepository : IBaseRepository<UserLogin, Guid>
    {
        /// <summary>
        /// Adds a new login to the table
        /// </summary>
        /// <param name="newLogin">New login to add</param>
        Task AddAsync(UserLogin newLogin);

        /// <summary>
        /// Updates specific properties of the login by its ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="loginId">ID of the login to update</param>
        /// <param name="propertiesAndValues">Expression to update properties</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> UpdateById(Guid loginId, Expression<Func<SetPropertyCalls<UserLogin>, SetPropertyCalls<UserLogin>>> propertiesAndValues);

        /// <summary>
        /// Deletes the login from the table
        /// </summary>
        /// <param name="login">Login to delete</param>
        void Delete(UserLogin login);
        /// <summary>
        /// Deletes the login from the table by its ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="loginId">ID of the login to delete</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteById(Guid loginId);
        /// <summary>
        /// Deletes all logins related to the user by the user's ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> DeleteAllByUserId(Guid userId);

        /// <summary>
        /// Gets the login by its ID
        /// </summary>
        /// <param name="loginId">ID of the login to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the login if it is found, otherwise NULL</returns>
        Task<UserLogin?> GetLoginById(Guid loginId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the login by its ID while selecting returned properties
        /// </summary>
        /// <typeparam name="T">Type that represents login properties to return</typeparam>
        /// <param name="loginId">ID of the login to find</param>
        /// <param name="select">Expression to select properties</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the selected properties of the login if it is found, otherwise NULL</returns>
        Task<T?> GetLoginById<T>(Guid loginId, Expression<Func<UserLogin, T>> select, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Gets the logins related to the user by the user's ID
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the logins related to the user if there is any, otherwise an empty list</returns>
        Task<IEnumerable<UserLogin>> GetLoginsByUserId(Guid userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the logins related to the user by the user's ID
        /// </summary>
        /// <typeparam name="T">Type that represents login properties to return</typeparam>
        /// <param name="userId">ID of the user to find</param>
        /// <param name="select">Expression to select properties</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the selected properties of the logins related to the user if there is any, otherwise an empty list</returns>
        Task<IEnumerable<T>> GetLoginsByUserId<T>(Guid userId, Expression<Func<UserLogin, T>> select, CancellationToken cancellationToken = default)
            where T : class;
    }
}
