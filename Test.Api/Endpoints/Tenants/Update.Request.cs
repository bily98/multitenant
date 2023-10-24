using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Endpoints.Tenants
{
    public class UpdateTenantRequest
    {
        [FromRoute] public int Id { get; set; }
        [FromBody] public UpdateTenantRequestBody Body { get; set; }
    }

    public class UpdateTenantRequestBody
    {
        public string Name { get; set; }
    }

    public class UpdateTenantRequestBodyValidation : AbstractValidator<UpdateTenantRequestBody>
    {
        public UpdateTenantRequestBodyValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
        }
    }
}
