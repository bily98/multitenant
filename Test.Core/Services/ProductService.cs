using Ardalis.Result;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.Core.Specifications.Products;
using Test.Core.Specifications.Tenants;
using Test.SharedKernel.Interfaces;

namespace Test.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IAppAsyncRepository<Product> _productRepository;
        private readonly ISecurityAsyncRepository<Tenant> _tenantRepository;

        public ProductService(IAppAsyncRepository<Product> productRepository, ISecurityAsyncRepository<Tenant> tenantRepository)
        {
            _productRepository = productRepository;
            _tenantRepository = tenantRepository;
        }

        public async Task<Result<IEnumerable<Product>>> GetAllAsync(string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<IEnumerable<Product>>.NotFound("Tenant not found");

                var productSpecification = new GetByTenantIdSpecification(tenant.Id);
                var products = await _productRepository.ListAsync(productSpecification);

                return Result<IEnumerable<Product>>.Success(products);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Product>>.Error(new[] { ex.Message });
            }
        }

        public async Task<Result<Product>> GetByIdAsync(int id, string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<Product>.NotFound("Tenant not found");

                var productSpecification = new GetByIdAndTenantIdSpecification(id, tenant.Id);
                var product = await _productRepository.FirstOrDefaultAsync(productSpecification);

                if (product == null)
                    return Result<Product>.NotFound("Product not found");

                return Result<Product>.Success(product);
            }
            catch (Exception ex)
            {
                return Result<Product>.Error(new[] { ex.Message });
            }
        }


        public async Task<Result<Product>> AddAsync(int userId, Product product, string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<Product>.NotFound("Tenant not found");

                product.TenantId = tenant.Id;
                product.CreatedBy = userId;
                product.CreatedAt = DateTime.UtcNow;

                product = await _productRepository.AddAsync(product);

                return Result<Product>.Success(product);
            }
            catch (Exception ex)
            {
                return Result<Product>.Error(new[] { ex.Message });
            }
        }

        public async Task<Result<Product>> UpdateAsync(int userId, Product product, string slugTenant)
        {
            try
            {
                var tenantSpecification = new GetBySlugSpecification(slugTenant);
                var tenant = await _tenantRepository.FirstOrDefaultAsync(tenantSpecification);

                if (tenant == null)
                    return Result<Product>.NotFound("Tenant not found");

                product.UpdatedBy = userId;
                product.UpdatedAt = DateTime.UtcNow;

                await _productRepository.UpdateAsync(product);

                return Result<Product>.Success(product);
            }
            catch (Exception ex)
            {
                return Result<Product>.Error(new[] { ex.Message });
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

                var productSpecification = new GetByIdAndTenantIdSpecification(id, tenant.Id);
                var product = await _productRepository.FirstOrDefaultAsync(productSpecification);

                if (product == null)
                    return Result<bool>.NotFound("Product not found");

                await _productRepository.DeleteAsync(product);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Error(new[] { ex.Message });
            }
        }
    }
}
