using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Users
{
    public class GetAll : EndpointBaseAsync.WithRequest<GetAllUserRequest>.WithActionResult<List<GetAllUserResponse>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAll(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet(Routes.Users.Get)]
        [SwaggerOperation(
                Summary = "Get All",
                Description = "This endpoint is used retrieve all users",
                OperationId = "Users.Get",
                Tags = new[] { "UserEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<List<GetAllUserResponse>>> HandleAsync([FromRoute] GetAllUserRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.GetAllAsync(request.SlugTenant);

            if (!result.IsSuccess)
            {
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);
            }

            var response = _mapper.Map<IEnumerable<GetAllUserResponse>>(result.Value);

            return Ok(response);
        }
    }
}
