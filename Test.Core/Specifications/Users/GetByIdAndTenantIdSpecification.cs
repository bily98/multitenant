using Ardalis.Specification;
using Test.Core.Entities;

namespace Test.Core.Specifications.Users
{
    public class GetByIdAndTenantIdSpecification : Specification<User>
    {
        public GetByIdAndTenantIdSpecification(int id, int tenantId)
        {
            Query.Where(x => x.Id == id && x.TenantId == tenantId);
        }
    }
}
