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
}
