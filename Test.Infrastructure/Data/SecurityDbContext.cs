using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Infrastructure.Extensions;

namespace Test.Infrastructure.Data
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.SetSimpleUnderscoreTableNameConvention(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
