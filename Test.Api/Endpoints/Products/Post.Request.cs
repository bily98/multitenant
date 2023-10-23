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
}
