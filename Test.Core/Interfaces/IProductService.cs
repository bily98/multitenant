using Ardalis.Result;
using Test.Core.Entities;

namespace Test.Core.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns a list of products</returns>
        Task<Result<IEnumerable<Product>>> GetAllAsync(string slugTenant);

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns a product</returns>
        Task<Result<Product>> GetByIdAsync(int id, string slugTenant);

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="product">Product object</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns the created product</returns>
        Task<Result<Product>> AddAsync(Product product, string slugTenant);

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="product">Product object with updated values</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns the updated product</returns>
        Task<Result<Product>> UpdateAsync(Product product, string slugTenant);

        /// <summary>
        /// Delete an existing product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="slugTenant">The slug tenant</param>
        /// <returns>Returns true if the product was deleted successfully</returns>
        Task<Result<bool>> DeleteAsync(int id, string slugTenant);
    }
}
