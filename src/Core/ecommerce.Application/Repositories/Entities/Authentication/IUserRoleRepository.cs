using ecommerce.Application.Models.Common;
using ecommerce.Domain.Entities.Authentication;
using System.Linq.Expressions;

namespace ecommerce.Application.Repositories.Entities.Authentication
{
    public interface IUserRoleRepository : IBaseRepository<UserRole, Guid>
    {
        /// <summary>
        /// Adds a new role to the table
        /// </summary>
        /// <param name="newRole">New role to add</param>
        Task AddAsync(UserRole newRole);

        /// <summary>
        /// Marks the role as deleted
        /// </summary>
        /// <param name="role">Role to mark as deleted</param>
        void SoftDelete(UserRole role);
        /// <summary>
        /// Finds the role in table and marks it as deleted
        /// </summary>
        /// <param name="roleId">ID of the role to delete</param>
        Task SoftDeleteById(Guid roleId);

        /// <summary>
        /// Gets the role by its ID
        /// </summary>
        /// <param name="roleId">ID of the role to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the role if it is found, otherwise NULL</returns>
        Task<UserRole?> GetRoleById(Guid roleId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the role by its name
        /// </summary>
        /// <param name="roleName">Name of the role to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the role if it is found, otherwise NULL</returns>
        Task<UserRole?> GetRoleByName(string roleName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the roles of the user by the user's ID
        /// </summary>
        /// <param name="userId">ID of the user to find</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the user roles if the user is found and there is any role, otherwise an empty list</returns>
        Task<IEnumerable<UserRole>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the users that have a specific role by the role's ID
        /// </summary>
        /// <param name="roleId">ID of the role to find</param>
        /// <param name="pagination">Limitation of the returned data</param>
        /// <param name="orderBy">Expression to order the result</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the users if there is any, otherwise an empty list</returns>
        Task<IEnumerable<User>> GetUsersByRoleId<T>(Guid roleId, Pagination pagination, Expression<Func<User, T>> orderBy, CancellationToken cancellationToken = default)
            where T : class;
        /// <summary>
        /// Gets the users that have a specific role by the role's ID
        /// </summary>
        /// <typeparam name="T">Type that represents user properties to return</typeparam>
        /// <param name="roleId">ID of the role to find</param>
        /// <param name="select">Expression to select properties</param>
        /// <param name="pagination">Limitation of the returned data</param>
        /// <param name="orderBy">Expression to order the result</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the selected properties of the users if there is any, otherwise an empty list</returns>
        Task<IEnumerable<T1>> GetUsersByRoleId<T1, T2>(Guid roleId, Expression<Func<User, T1>> select, Pagination pagination, Expression<Func<T1, T2>> orderBy, CancellationToken cancellationToken = default)
            where T1 : class
            where T2 : class;
        /// <summary>
        /// Gets the users that have a specific role by the role's name
        /// </summary>
        /// <param name="roleName">Name of the role to find</param>
        /// <param name="pagination">Limitation of the returned data</param>
        /// <param name="orderBy">Expression to order the result</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the users if there is any, otherwise an empty list</returns>
        Task<IEnumerable<User>> GetUsersByRoleName<T>(string roleName, Pagination pagination, Expression<Func<User, T>> orderBy, CancellationToken cancellationToken = default)
            where T : class;
        /// <summary>
        /// Gets the users that have a specific role by the role's name
        /// </summary>
        /// <typeparam name="T">Type that represents user properties to return</typeparam>
        /// <param name="roleName">Name of the role to find</param>
        /// <param name="select">Expression to select properties</param>
        /// <param name="pagination">Limitation of the returned data</param>
        /// <param name="orderBy">Expression to order the result</param>
        /// <param name="cancellationToken">Token to check for the cancellation request</param>
        /// <returns>Returns the list of the selected properties of the users if there is any, otherwise an empty list</returns>
        Task<IEnumerable<T1>> GetUsersByRoleName<T1, T2>(string roleName, Expression<Func<User, T1>> select, Pagination pagination, Expression<Func<T1, T2>> orderBy, CancellationToken cancellationToken = default)
            where T1 : class
            where T2 : class;
    }
}
