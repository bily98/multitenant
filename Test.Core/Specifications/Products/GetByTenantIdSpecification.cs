using Ardalis.Specification;
using Test.Core.Entities;

namespace Test.Core.Specifications.Products
{
    public class GetByTenantIdSpecification : Specification<Product>
    {
        public GetByTenantIdSpecification(int tenantId)
        {
            Query.Where(x => x.TenantId == tenantId);
        }
    }
}
