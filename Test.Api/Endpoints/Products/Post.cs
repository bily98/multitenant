using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Entities;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Products
{
    public class Post : EndpointBaseAsync.WithRequest<PostProductRequest>.WithActionResult<PostProductResponse>
    {
        private readonly IProductService _productService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public Post(IProductService productService, IPasswordService passwordService,
            IMapper mapper)
        {
            _productService = productService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        [HttpPost(Routes.Products.Create)]
        [SwaggerOperation(
                Summary = "Create new product",
                Description = "This endpoint is used to create a new product",
                OperationId = "Products.Create",
                Tags = new[] { "ProductEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<PostProductResponse>> HandleAsync([FromRoute] PostProductRequest request, CancellationToken cancellationToken = default)
        {
            var product = _mapper.Map<Product>(request.Body);

            var result = await _productService.AddAsync(product, request.SlugTenant);

            if (!result.IsSuccess)
            {
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);
            }

            var response = _mapper.Map<PostProductResponse>(result.Value);

            return Created("", response);
        }
    }
}
