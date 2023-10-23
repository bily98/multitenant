using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Products
{
    public class GetAll : EndpointBaseAsync.WithRequest<GetAllProductRequest>.WithActionResult<List<GetAllProductResponse>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public GetAll(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet(Routes.Products.Get)]
        [SwaggerOperation(
                Summary = "Get All",
                Description = "This endpoint is used retrieve all products",
                OperationId = "Products.Get",
                Tags = new[] { "ProductEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<List<GetAllProductResponse>>> HandleAsync([FromRoute] GetAllProductRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _productService.GetAllAsync(request.SlugTenant);

            if (!result.IsSuccess)
            {
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);
            }

            var response = _mapper.Map<IEnumerable<GetAllProductResponse>>(result.Value);

            return Ok(response);
        }
    }
}
