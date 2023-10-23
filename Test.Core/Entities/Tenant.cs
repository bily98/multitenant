using Test.SharedKernel.Entities;

namespace Test.Core.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
