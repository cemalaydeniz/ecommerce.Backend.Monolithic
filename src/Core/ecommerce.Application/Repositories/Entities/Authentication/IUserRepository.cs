using ecommerce.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ecommerce.Application.Repositories.Entities.Authentication
{
    public interface IUserRepository<TEntity> : IBaseRepository<TEntity, Guid>
        where TEntity : User
    {
        /// <summary>
        /// Adds a new user to the table
        /// </summary>
        /// <param name="newUser">New user to add</param>
        Task AddAsync(TEntity newUser);

        /// <summary>
        /// Updates specific properties of the user by its ID immediately. <see cref="IBaseRepository{TEntity, TKey}.SaveChangesAsync"/> is not required after calling this method.
        /// This method also only updates the table of the entity. In case the entity is represented more than one table, related repositories should be used for updating by ID
        /// </summary>
        /// <param name="userId">ID of the user to update</param>
        /// <param name="propertiesAndValues">Expression to update properties</param>
        /// <returns>Returns the number of affected rows</returns>
        Task<int> UpdateById(Guid userId, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> propertiesAndValues);

        /// <summary>
        /// Marks the user as deleted and sets the nullable columns as null. Navigation properties must be cleared manually
        /// </summary>
        /// <param name="user">User to mark as deleted</param>
        void SoftDelete(TEntity user);
        /// <summary>
        /// Finds the user and marks it as deleted and sets the nullable columns as null. Navigation properties must be cleared manually
        /// </summary>
        /// <param name="userId">ID of the user to delete</param>
        Task SoftDeleteById(Guid userId);

        /// <summary>
        /// Gets the user by its ID
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the user if it is found, otherwise NULL</returns>
        Task<TEntity?> GetUserById(Guid userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the user by its ID while selecting returned properties
        /// </summary>
        /// <typeparam name="T">Type that represents user properties to return</typeparam>
        /// <param name="userId">ID of the user ID to find</param>
        /// <param name="select">Expression to select properties</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the selected properties of the user if it is found, otherwise NULL</returns>
        Task<T?> GetUserById<T>(Guid userId, Expression<Func<TEntity, T>> select, CancellationToken cancellationToken = default)
            where T : class;
        /// <summary>
        /// Gets the user by its email
        /// </summary>
        /// <param name="email">Email of the user to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the user if it is found, otherwise NULL</returns>
        Task<TEntity?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the user by its email while selecting returned properties
        /// </summary>
        /// <typeparam name="T">Type that represents user properties to return</typeparam>
        /// <param name="email">Email of the user to find</param>
        /// <param name="select">Expression to select properties</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the selected properties of the user if it is found, otherwise NULL</returns>
        Task<T?> GetUserByEmail<T>(string email, Expression<Func<TEntity, T>> select, CancellationToken cancellationToken = default)
            where T : class;
    }
}
