using Test.Core.Entities;
using Test.Infrastructure.Interface;

namespace Test.Infrastructure.Providers
{
    public class TenantProvider : ITenantProvider
    {
        private static Tenant _tenant;

        public Tenant GetTenant()
        {
            return _tenant;
        }

        public void SetTenant(Tenant tenant)
        {
            _tenant = tenant;
        }
    }
}
