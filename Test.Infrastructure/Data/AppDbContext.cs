using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Infrastructure.Extensions;

namespace Test.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.SetSimpleUnderscoreTableNameConvention(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
