using Test.Core.Entities;

namespace Test.Infrastructure.Interface
{
    public interface ITenantProvider
    {
        /// <summary>
        /// Set the tenant for the current request
        /// </summary>
        /// <param name="tenant">The tenant to set for the current request</param>
        void SetTenant(Tenant tenant);

        /// <summary>
        /// Get the tenant for the current request
        /// </summary>
        /// <returns>Returns the tenant for the current request</returns>
        Tenant GetTenant();
    }
}
