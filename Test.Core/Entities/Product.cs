using Test.SharedKernel.Entities;

namespace Test.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int TenantId { get; set; }
    }
}
