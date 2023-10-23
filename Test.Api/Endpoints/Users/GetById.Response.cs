namespace Test.Api.Endpoints.Users
{
    public class GetByIdUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TenantName { get; set; }
    }
}
