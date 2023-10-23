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
}
