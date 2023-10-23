using Test.SharedKernel.Entities;

namespace Test.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
