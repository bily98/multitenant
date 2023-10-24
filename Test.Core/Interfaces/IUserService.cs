using Ardalis.Result;
using Test.Core.Entities;

namespace Test.Core.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns a list of users</returns>
        Task<Result<IEnumerable<User>>> GetAllAsync(string slugTenant);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns a user</returns>
        Task<Result<User>> GetByIdAsync(int id, string slugTenant);

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="userId">The id of user logged</param>
        /// <param name="user">User object</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns the created user</returns>
        Task<Result<User>> AddAsync(int userId, User user, string slugTenant);

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="userId">The id of user logged</param>
        /// <param name="user">User object with updated values</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns the updated user</returns>
        Task<Result<User>> UpdateAsync(int userId, User user, string slugTenant);

        /// <summary>
        /// Delete an existing user
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns true if the user was deleted successfully</returns>
        Task<Result<bool>> DeleteAsync(int id, string slugTenant);
    }
}
