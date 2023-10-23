using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Products
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdProductRequest>.WithActionResult<GetByIdProductResponse>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public GetById(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet(Routes.Products.GetById)]
        [SwaggerOperation(
                Summary = "Get by Id",
                Description = "This endpoint is used retrieve a product by id",
                OperationId = "Products.GetById",
                Tags = new[] { "ProductEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<GetByIdProductResponse>> HandleAsync([FromRoute] GetByIdProductRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _productService.GetByIdAsync(request.Id, request.SlugTenant);

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            if (result.Value == null)
                return NotFound();

            var response = _mapper.Map<GetByIdProductResponse>(result.Value);

            return Ok(response);
        }
    }
}
