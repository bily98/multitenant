using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Endpoints.Users
{
    public class PostUserRequest
    {
        public string SlugTenant { get; set; }
        [FromBody] public PostUserRequestBody Body { get; set; }
    }

    public class PostUserRequestBody
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class PostUserRequestBodyValidation : AbstractValidator<PostUserRequestBody>
    {
        public PostUserRequestBodyValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email must be valid");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password should not be empty");
        }
    }
}
