using Ardalis.Specification;
using Test.Core.Entities;

namespace Test.Core.Specifications.Users
{
    public class GetByTenantIdSpecification : Specification<User>
    {
        public GetByTenantIdSpecification(int tenantId)
        {
            Query.Where(x => x.TenantId == tenantId);
        }
    }
}
