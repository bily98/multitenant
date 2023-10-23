using Ardalis.Result;
using Test.Core.Entities;

namespace Test.Core.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <param name="password">The user password.</param>
        /// <returns>Return user if login success.</returns>
        Task<Result<User>> Login(string email, string password);
    }
}
