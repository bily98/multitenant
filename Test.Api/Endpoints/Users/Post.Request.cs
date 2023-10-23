using Microsoft.AspNetCore.Mvc;

namespace Test.Api.Endpoints.Users
{
    public class PostUserRequest
    {
        public string SlugTenant { get; set; }
        [FromBody] public PostUserRequestBody Body { get; set; }
    }

    public class PostUserRequestBody
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
