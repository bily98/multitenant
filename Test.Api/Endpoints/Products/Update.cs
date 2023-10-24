using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Products
{
    public class Update : EndpointBaseAsync.WithRequest<UpdateProductRequest>.WithActionResult<UpdateProductResponse>
    {
        private readonly IProductService _productService;
        private readonly IValidator<UpdateProductRequestBody> _validator;
        private readonly IMapper _mapper;

        public Update(IProductService productService, IValidator<UpdateProductRequestBody> validator,
            IMapper mapper)
        {
            _productService = productService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPut(Routes.Products.Update)]
        [SwaggerOperation(
                Summary = "Update a product",
                Description = "This endpoint is used to update a product",
                OperationId = "Products.Update",
                Tags = new[] { "ProductEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<UpdateProductResponse>> HandleAsync([FromRoute] UpdateProductRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request.Body);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var productResult = await _productService.GetByIdAsync(request.Id, request.SlugTenant);

            if (!productResult.IsSuccess)
                return Problem(string.Join(",", productResult.Errors), null, StatusCodes.Status500InternalServerError);

            if (productResult.Value == null)
                return NotFound();

            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var product = _mapper.Map(request.Body, productResult.Value);

            var result = await _productService.UpdateAsync(Convert.ToInt32(userId), product, request.SlugTenant);

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            var response = _mapper.Map<UpdateProductResponse>(result.Value);

            return Ok(response);
        }
    }
}
