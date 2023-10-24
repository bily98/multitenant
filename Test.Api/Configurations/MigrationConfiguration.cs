using Microsoft.EntityFrameworkCore;
using Test.Infrastructure.Data;

namespace Test.Api.Configurations
{
    public static class MigrationConfiguration
    {
        /// <summary>
        /// This method will migrate the database if it's in development, otherwise EnsureCreated
        /// </summary>
        /// <param name="serviceProvider">
        /// The Service Provider.
        /// </param>
        public static void MigrateSecurityDbContext(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                var securityContext = services.GetRequiredService<SecurityDbContext>();
                var environment = services.GetRequiredService<IWebHostEnvironment>();

                // Migrate the database if it's in development, otherwise EnsureCreated
                if (environment.IsDevelopment())
                {
                    securityContext.Database.Migrate();
                }
                else
                {
                    securityContext.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrating the DB.");
            }
        }
        /// <summary>
        /// This method will migrate the database if it's in development, otherwise EnsureCreated
        /// </summary>
        /// <param name="serviceProvider">
        /// The Service Provider.
        /// </param>
        public static void MigrateAppDbContext(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                var AppContext = services.GetRequiredService<AppDbContext>();
                var environment = services.GetRequiredService<IWebHostEnvironment>();

                // Migrate the database if it's in development, otherwise EnsureCreated
                if (environment.IsDevelopment())
                {
                    AppContext.Database.Migrate();
                }
                else
                {
                    AppContext.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred migrating the DB.");
            }
        }
    }
}
