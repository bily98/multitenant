using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.Infrastructure.Data;
using Test.SharedKernel.Entities;
using Test.SharedKernel.Interfaces;

namespace Test.Infrastructure.Repositories
{
    public class AppAsyncRepository<T> : RepositoryBase<T>, IAppAsyncRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public AppAsyncRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateDatabaseAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}
