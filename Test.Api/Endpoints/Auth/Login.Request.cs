using FluentValidation;

namespace Test.Api.Endpoints.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email must be valid");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password should not be empty");
        }
    }
}
