using Ardalis.Result;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.Core.Specifications.Tenants;
using Test.Core.Specifications.Users;
using Test.SharedKernel.Interfaces;

namespace Test.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ISecurityAsyncRepository<User> _userRepository;
        private readonly ISecurityAsyncRepository<Tenant> _tenantRepository;

        public UserService(ISecurityAsyncRepository<User> userRepository, ISecurityAsyncRepository<Tenant> tenantRepository)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
        }

        public async Task<Result<IEnumerable<User>>> GetAllAsync(string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<IEnumerable<User>>.NotFound("Tenant not found");

                var userSpecification = new GetByTenantIdSpecification(tenant.Id);
                var users = await _userRepository.ListAsync(userSpecification);

                return Result<IEnumerable<User>>.Success(users);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<User>>.Error(new[] { ex.Message });
            }
        }

        public async Task<Result<User>> GetByIdAsync(int id, string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<User>.NotFound("Tenant not found");

                var userSpecification = new GetByIdAndTenantIdSpecification(id, tenant.Id);
                var user = await _userRepository.FirstOrDefaultAsync(userSpecification);

                if (user == null)
                    return Result<User>.NotFound("User not found");

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Error(new[] { ex.Message });
            }
        }


        public async Task<Result<User>> AddAsync(User user, string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<User>.NotFound("Tenant not found");

                user.TenantId = tenant.Id;
                user.CreatedBy = 1;
                user.CreatedAt = DateTime.UtcNow;

                user = await _userRepository.AddAsync(user);

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Error(new[] { ex.Message });
            }
        }

        public async Task<Result<User>> UpdateAsync(User user, string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<User>.NotFound("Tenant not found");

                tenant.UpdatedBy = 1;
                tenant.UpdatedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Error(new[] { ex.Message });
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id, string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<bool>.NotFound("Tenant not found");

                var userSpecification = new GetByIdAndTenantIdSpecification(id, tenant.Id);
                var user = await _userRepository.FirstOrDefaultAsync(userSpecification);

                if (user == null)
                    return Result<bool>.NotFound("User not found");

                await _userRepository.DeleteAsync(user);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(new[] { ex.Message });
            }
        }
    }
}
