using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Entities;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Tenants
{
    public class Post : EndpointBaseAsync.WithRequest<PostTenantRequest>.WithActionResult<PostTenantResponse>
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public Post(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        [HttpPost(Routes.Tenants.Create)]
        [SwaggerOperation(
                Summary = "Create new tenant",
                Description = "This endpoint is used to create a new tenant",
                OperationId = "Tenants.Create",
                Tags = new[] { "TenantEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<PostTenantResponse>> HandleAsync(PostTenantRequest request, CancellationToken cancellationToken = default)
        {
            var tenant = _mapper.Map<Tenant>(request);

            var result = await _tenantService.AddAsync(tenant);

            if (!result.IsSuccess)
            {
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);
            }

            var response = _mapper.Map<PostTenantResponse>(result.Value);

            return Created("", response);
        }
    }
}
