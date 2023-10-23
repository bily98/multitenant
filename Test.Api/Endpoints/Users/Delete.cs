using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Users
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteUserRequest>.WithoutResult
    {
        private readonly IUserService _userService;

        public Delete(IUserService userService)
        {
            _userService = userService;
        }

        [HttpDelete(Routes.Users.Delete)]
        [SwaggerOperation(
                Summary = "Delete a user",
                Description = "This endpoint is used to delete a user",
                OperationId = "Users.Delete",
                Tags = new[] { "UserEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult> HandleAsync([FromRoute] DeleteUserRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.DeleteAsync(request.Id, request.SlugTenant);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}
