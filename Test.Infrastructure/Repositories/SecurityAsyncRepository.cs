using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.Infrastructure.Data;
using Test.SharedKernel.Entities;
using Test.SharedKernel.Interfaces;

namespace Test.Infrastructure.Repositories
{
    public class SecurityAsyncRepository<T> : RepositoryBase<T>, ISecurityAsyncRepository<T> where T : BaseEntity
    {
        public SecurityAsyncRepository(SecurityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
