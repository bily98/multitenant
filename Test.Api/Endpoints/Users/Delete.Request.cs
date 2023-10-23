namespace Test.Api.Endpoints.Users
{
    public class DeleteUserRequest
    {
        public int Id { get; set; }
        public string SlugTenant { get; set; }
    }
}
