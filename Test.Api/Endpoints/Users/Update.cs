using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Users
{
    public class Update : EndpointBaseAsync.WithRequest<UpdateUserRequest>.WithActionResult<UpdateUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public Update(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPut(Routes.Users.Update)]
        [SwaggerOperation(
                Summary = "Update a user",
                Description = "This endpoint is used to update a user",
                OperationId = "Users.Update",
                Tags = new[] { "UserEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<UpdateUserResponse>> HandleAsync([FromRoute] UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            var userResult = await _userService.GetByIdAsync(request.Id, request.SlugTenant);

            if (!userResult.IsSuccess)
                return Problem(string.Join(",", userResult.Errors), null, StatusCodes.Status500InternalServerError);

            if (userResult.Value == null)
                return NotFound();

            var user = _mapper.Map(request.Body, userResult.Value);

            var result = await _userService.UpdateAsync(user, request.SlugTenant);

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            var response = _mapper.Map<UpdateUserResponse>(result.Value);

            return Ok(response);
        }
    }
}
