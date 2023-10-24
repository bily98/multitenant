using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using Test.Api.Configurations;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.Infrastructure.Interface;

namespace Test.Api.Endpoints.Tenants
{
    public class Post : EndpointBaseAsync.WithRequest<PostTenantRequest>.WithActionResult<PostTenantResponse>
    {
        private readonly ITenantService _tenantService;
        private readonly ITenantProvider _tenantProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IValidator<PostTenantRequest> _validator;
        private readonly IMapper _mapper;

        public Post(ITenantService tenantService, ITenantProvider tenantProvider,
            IServiceProvider serviceProvider, IValidator<PostTenantRequest> validator,
            IMapper mapper)
        {
            _tenantService = tenantService;
            _tenantProvider = tenantProvider;
            _serviceProvider = serviceProvider;
            _validator = validator;
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
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var tenant = _mapper.Map<Tenant>(request);

            var result = await _tenantService.AddAsync(Convert.ToInt32(userId), tenant);

            if (!result.IsSuccess)
            {
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);
            }

            _tenantProvider.SetTenant(tenant);
            _serviceProvider.MigrateAppDbContext();

            var response = _mapper.Map<PostTenantResponse>(result.Value);

            return Created("", response);
        }
    }
}
