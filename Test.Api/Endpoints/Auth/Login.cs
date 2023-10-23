using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Core.Entities;
using Test.Core.Interfaces;

namespace Test.Api.Endpoints.Auth
{
    public class Login : EndpointBaseAsync.WithRequest<LoginRequest>.WithActionResult<LoginResponse>
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public Login(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;

        }

        [HttpPost(Routes.Auth.Login)]
        [SwaggerOperation(
                Summary = "Login",
                Description = "This endpoint is used to login",
                OperationId = "Auth.Login",
                Tags = new[] { "AuthEndpoints" }),
        ]
        public override async Task<ActionResult<LoginResponse>> HandleAsync([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _authService.Login(request.Email, request.Password);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            if (result.Status == ResultStatus.Invalid)
                return BadRequest(result.ValidationErrors);

            if (!result.IsSuccess)
                return Problem(string.Join(",", result.Errors), null, StatusCodes.Status500InternalServerError);

            return await GenerateTokenAsync(result.Value);
        }

        private async Task<ActionResult<LoginResponse>> GenerateTokenAsync(User user)
        {
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // Claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, $"{user.Name}"),
                new(ClaimTypes.NameIdentifier, $"{user.Id}")
            };

            // Payload
            var payload = new JwtPayload(_configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims, DateTime.Now, DateTime.UtcNow.AddMinutes(15));

            var securityToken = new JwtSecurityToken(header, payload);

            var response = new LoginResponse
            {
                Name = user.Name,
                SlugTenant = user.Tenant.Slug,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };

            return Ok(response);
        }
    }
}
