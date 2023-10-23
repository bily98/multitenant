using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Core.Entities;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Users
{
    public class Post : EndpointBaseAsync.WithRequest<PostUserRequest>.WithActionResult<PostUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public Post(IUserService userService, IPasswordService passwordService,
            IMapper mapper)
        {
            _userService = userService;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        [HttpPost(Routes.Users.Create)]
        [SwaggerOperation(
                Summary = "Create new user",
                Description = "This endpoint is used to create a new user",
                OperationId = "Users.Create",
                Tags = new[] { "UserEndpoints" }),
        ]
        [Authorize]
        public override async Task<ActionResult<PostUserResponse>> HandleAsync([FromRoute] PostUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(request.Body);
            user.Password = _passwordService.Hash(user.Password);

            var result = await _userService.AddAsync(user, request.SlugTenant);

            if (!result.IsSuccess)
            {
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);
            }

            var response = _mapper.Map<PostUserResponse>(result.Value);

            return Created("", response);
        }
    }
}
