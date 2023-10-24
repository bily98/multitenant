using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Test.Core.Entities;
using Test.Infrastructure.Extensions;
using Test.Infrastructure.Interface;

namespace Test.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly Tenant _tenant;

        public AppDbContext(ITenantProvider tenantProvider, DbContextOptions options) : base(options)
        {
            _tenant = tenantProvider.GetTenant();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_tenant.ConnectionString, connectionOptions =>
            {
                connectionOptions.MigrationsAssembly("Test.Infrastructure.Migrations");
            });

            optionsBuilder.UseSnakeCaseNamingConvention();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.SetSimpleUnderscoreTableNameConvention(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
