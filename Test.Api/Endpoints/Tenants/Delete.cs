using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Tenants
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteTenantRequest>.WithoutResult
    {
        private readonly ITenantService _tenantService;

        public Delete(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpDelete(Routes.Tenants.Delete)]
        [SwaggerOperation(
                Summary = "Delete a tenant",
                Description = "This endpoint is used to delete a tenant",
                OperationId = "Tenants.Delete",
                Tags = new[] { "TenantEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult> HandleAsync([FromRoute] DeleteTenantRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _tenantService.DeleteAsync(request.Id);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}
