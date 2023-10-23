using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Products
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteProductRequest>.WithoutResult
    {
        private readonly IProductService _productService;

        public Delete(IProductService productService)
        {
            _productService = productService;
        }

        [HttpDelete(Routes.Products.Delete)]
        [SwaggerOperation(
                Summary = "Delete a product",
                Description = "This endpoint is used to delete a product",
                OperationId = "Products.Delete",
                Tags = new[] { "ProductEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult> HandleAsync([FromRoute] DeleteProductRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _productService.DeleteAsync(request.Id, request.SlugTenant);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}
