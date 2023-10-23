using Ardalis.Specification;
using Test.Core.Entities;

namespace Test.Core.Specifications.Products
{
    public class GetByIdAndTenantIdSpecification : Specification<Product>
    {
        public GetByIdAndTenantIdSpecification(int id, int tenantId)
        {
            Query.Where(x => x.Id == id && x.TenantId == tenantId);
        }
    }
}
