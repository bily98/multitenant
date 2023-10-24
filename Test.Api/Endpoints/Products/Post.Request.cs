using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Endpoints.Products
{
    public class PostProductRequest
    {
        public string SlugTenant { get; set; }
        [FromBody] public PostProductRequestBody Body { get; set; }
    }

    public class PostProductRequestBody
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class PostProductRequestBodyValidation : AbstractValidator<PostProductRequestBody>
    {
        public PostProductRequestBodyValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empdy");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be grater than 0");
        }
    }
}
