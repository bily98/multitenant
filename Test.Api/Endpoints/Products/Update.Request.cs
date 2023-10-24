using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Endpoints.Products
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string SlugTenant { get; set; }
        [FromBody] public UpdateProductRequestBody Body { get; set; }
    }

    public class UpdateProductRequestBody
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class UpdateProductRequestBodyValidation : AbstractValidator<UpdateProductRequestBody>
    {
        public UpdateProductRequestBodyValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empdy");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be grater than 0");
        }
    }
}
