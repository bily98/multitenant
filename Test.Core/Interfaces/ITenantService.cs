using Ardalis.Result;
using Test.Core.Entities;

namespace Test.Core.Interfaces
{
    public interface ITenantService
    {
        /// <summary>
        /// Get all tenants
        /// </summary>
        /// <returns>Returns a list of tenants</returns>
        Task<Result<IEnumerable<Tenant>>> GetAllAsync();

        /// <summary>
        /// Get tenant by id
        /// </summary>
        /// <param name="id">Tenant id</param>
        /// <returns>Returns a tenant</returns>
        Task<Result<Tenant>> GetByIdAsync(int id);

        /// <summary>
        /// Get tenant by id
        /// </summary>
        /// <param name="id">Tenant id</param>
        /// <returns>Returns a tenant</returns>
        Task<Result<Tenant>> GetBySlugAsync(string slug);

        /// <summary>
        /// Add a new tenant
        /// </summary>
        /// <param name="tenant">Tenant object</param>
        /// <returns>Returns the created tenant</returns>
        Task<Result<Tenant>> AddAsync(Tenant tenant);

        /// <summary>
        /// Update an existing tenant
        /// </summary>
        /// <param name="tenant">Tenant object with updated values</param>
        /// <returns>Returns the updated tenant</returns>
        Task<Result<Tenant>> UpdateAsync(Tenant tenant);

        /// <summary>
        /// Delete an existing tenant
        /// </summary>
        /// <param name="id">Tenant id</param>
        /// <returns>Returns true if the tenant was deleted successfully</returns>
        Task<Result<bool>> DeleteAsync(int id);
    }
}
