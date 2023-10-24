using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Infrastructure.Extensions;
using Test.SharedKernel.Entities;

namespace Test.Infrastructure.Data
{
    public class SecurityDbContext : DbContext
    {
        private User _user = new User()
        {
            Id = 1,
            Name = "Admin",
            Email = "admin@admin.com",
            CreatedBy = 0,
            CreatedAt = DateTime.UtcNow,
            Password = "AXsylzBoK0cEbxWE3TWzMA==.09d+cQlb2zsQEWgsiJgeNTDhZRJkA79Qeq20llsJIgg="
        };

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.SetSimpleUnderscoreTableNameConvention(true);

            modelBuilder.Entity<User>(x => x.HasData(_user));

            base.OnModelCreating(modelBuilder);
        }
    }
}
