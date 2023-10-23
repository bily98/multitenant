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
        public string Slug { get; set; }
    }
}
