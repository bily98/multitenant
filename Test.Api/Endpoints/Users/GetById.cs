using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Users
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdUserRequest>.WithActionResult<GetByIdUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetById(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet(Routes.Users.GetById)]
        [SwaggerOperation(
                Summary = "Get by Id",
                Description = "This endpoint is used retrieve a user by id",
                OperationId = "Users.GetById",
                Tags = new[] { "UserEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<GetByIdUserResponse>> HandleAsync([FromRoute] GetByIdUserRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.GetByIdAsync(request.Id, request.SlugTenant);

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            if (result.Value == null)
                return NotFound();

            var response = _mapper.Map<GetByIdUserResponse>(result.Value);

            return Ok(response);
        }
    }
}
