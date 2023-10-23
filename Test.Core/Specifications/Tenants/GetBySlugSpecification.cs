using Ardalis.Specification;
using Test.Core.Entities;

namespace Test.Core.Specifications.Tenants
{
    public class GetBySlugSpecification : Specification<Tenant>
    {
        public GetBySlugSpecification(string slug)
        {
            Query.Where(x => x.Slug == slug);
        }
    }
}
