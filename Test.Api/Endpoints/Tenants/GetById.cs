using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Tenants
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdTenantRequest>.WithActionResult<GetByIdTenantResponse>
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetById(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        [HttpGet(Routes.Tenants.GetById)]
        [SwaggerOperation(
                Summary = "Get by Id",
                Description = "This endpoint is used retrieve a tenant by id",
                OperationId = "Tenants.GetById",
                Tags = new[] { "TenantEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<GetByIdTenantResponse>> HandleAsync([FromRoute] GetByIdTenantRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _tenantService.GetByIdAsync(request.Id);

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            if (result.Value == null)
                return NotFound();

            var response = _mapper.Map<GetByIdTenantResponse>(result.Value);

            return Ok(response);
        }
    }
}
