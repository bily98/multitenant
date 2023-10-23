using Ardalis.Specification;
using Test.Core.Entities;

namespace Test.Core.Specifications.Users
{
    public class GetByEmailSpecification : Specification<User>, ISingleResultSpecification<User>
    {
        public GetByEmailSpecification(string email)
        {
            Query.Include(x => x.Tenant);

            Query.Where(x => x.Email == email);
        }
    }
}
