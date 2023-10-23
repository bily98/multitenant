using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Tenants
{
    public class Update : EndpointBaseAsync.WithRequest<UpdateTenantRequest>.WithActionResult<UpdateTenantResponse>
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public Update(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        [HttpPut(Routes.Tenants.Update)]
        [SwaggerOperation(
                Summary = "Update a tenant",
                Description = "This endpoint is used to update a tenant",
                OperationId = "Tenants.Update",
                Tags = new[] { "TenantEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<UpdateTenantResponse>> HandleAsync([FromRoute] UpdateTenantRequest request, CancellationToken cancellationToken = default)
        {
            var tenantResult = await _tenantService.GetByIdAsync(request.Id);

            if (!tenantResult.IsSuccess)
                return Problem(string.Join(",", tenantResult.Errors), null, StatusCodes.Status500InternalServerError);

            if (tenantResult.Value == null)
                return NotFound();

            var tenant = _mapper.Map(request.Body, tenantResult.Value);

            var result = await _tenantService.UpdateAsync(tenant);

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            var response = _mapper.Map<UpdateTenantResponse>(result.Value);

            return Ok(response);
        }
    }
}
