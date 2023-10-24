using FluentValidation;

namespace Test.Api.Endpoints.Tenants
{
    public class PostTenantRequest
    {
        public string Name { get; set; }
    }

    public class PostTenantRequestValidation : AbstractValidator<PostTenantRequest>
    {
        public PostTenantRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
        }
    }
}
