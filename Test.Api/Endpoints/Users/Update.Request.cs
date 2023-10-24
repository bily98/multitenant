using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Endpoints.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string SlugTenant { get; set; }
        [FromBody] public UpdateUserRequestBody Body { get; set; }
    }

    public class UpdateUserRequestBody
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UpdateUserRequestBodyValidation : AbstractValidator<UpdateUserRequestBody>
    {
        public UpdateUserRequestBodyValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email must be valid");
        }
    }
}
