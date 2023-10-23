namespace Test.Api.Endpoints.Products
{
    public class PostProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string TenantName { get; set; }
    }
}
