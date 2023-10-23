using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Tenants
{
    public class GetAll : EndpointBaseAsync.WithoutRequest.WithActionResult<List<GetAllTenantResponse>>
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetAll(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        [HttpGet(Routes.Tenants.Get)]
        [SwaggerOperation(
                Summary = "Get All",
                Description = "This endpoint is used retrieve all tenants",
                OperationId = "Tenants.Get",
                Tags = new[] { "TenantEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<List<GetAllTenantResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var result = await _tenantService.GetAllAsync();

            if (!result.IsSuccess)
            {
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);
            }

            var response = _mapper.Map<IEnumerable<GetAllTenantResponse>>(result.Value);

            return Ok(response);
        }
    }
}
