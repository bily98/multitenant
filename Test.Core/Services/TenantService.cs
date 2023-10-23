using Ardalis.Result;
using AutoMapper;
using Microsoft.VisualBasic;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.Core.Specifications.Tenants;
using Test.SharedKernel.Interfaces;

namespace Test.Core.Services
{
    public class TenantService : ITenantService
    {
        private readonly ISecurityAsyncRepository<Tenant> _tenantRepository;
        private readonly IMapper _mapper;

        public TenantService(ISecurityAsyncRepository<Tenant> tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<Tenant>>> GetAllAsync()
        {
            try
            {
                var tenants = await _tenantRepository.ListAsync();

                return Result<IEnumerable<Tenant>>.Success(tenants);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<Tenant>>.Error(e.Message);
            }
        }

        public async Task<Result<Tenant>> GetByIdAsync(int id)
        {
            try
            {
                var tenant = await _tenantRepository.GetByIdAsync(id);

                if (tenant == null)
                    return Result<Tenant>.NotFound();

                return Result<Tenant>.Success(tenant);
            }
            catch (Exception e)
            {
                return Result<Tenant>.Error(e.Message);
            }
        }

        public async Task<Result<Tenant>> GetBySlugAsync(string slug)
        {
            try
            {
                var specification = new GetBySlugSpecification(slug);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(specification);

                if (tenant == null)
                    return Result<Tenant>.NotFound();

                return Result<Tenant>.Success(tenant);
            }
            catch (Exception e)
            {
                return Result<Tenant>.Error(e.Message);
            }
        }


        public async Task<Result<Tenant>> AddAsync(Tenant tenant)
        {
            try
            {
                tenant.CreatedBy = 1;
                tenant.CreatedAt = DateTime.UtcNow;

                tenant = await _tenantRepository.AddAsync(tenant);

                return Result<Tenant>.Success(tenant);
            }
            catch (Exception e)
            {
                return Result<Tenant>.Error(e.Message);
            }
        }

        public async Task<Result<Tenant>> UpdateAsync(Tenant tenant)
        {
            try
            {
                tenant.UpdatedBy = 1;
                tenant.UpdatedAt = DateTime.UtcNow;

                await _tenantRepository.UpdateAsync(tenant);

                return Result<Tenant>.Success(tenant);
            }
            catch (Exception e)
            {
                return Result<Tenant>.Error(e.Message); 
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var tenant = await _tenantRepository.GetByIdAsync(id);

                if (tenant == null)
                    return Result<bool>.NotFound();

                await _tenantRepository.DeleteAsync(tenant);

                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Error(e.Message);
            }
        }
    }
}
