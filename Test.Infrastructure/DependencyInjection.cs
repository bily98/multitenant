using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Infrastructure.Data;
using Test.Infrastructure.Interface;
using Test.Infrastructure.Providers;
using Test.Infrastructure.Repositories;
using Test.SharedKernel.Interfaces;

namespace Test.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add all services to DI container
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">The configuration builder.</param>
        /// <returns>Return service collection</returns>
        public static IServiceCollection AddTestInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SecurityConnection"), connectionOptions =>
                {
                    connectionOptions.MigrationsAssembly("Test.Infrastructure.Migrations");
                });
                options.UseSnakeCaseNamingConvention();
            });

            services.AddDbContext<AppDbContext>();

            services.AddTransient<ITenantProvider, TenantProvider>();

            services.AddScoped(typeof(IAppAsyncRepository<>), typeof(AppAsyncRepository<>));
            services.AddScoped(typeof(ISecurityAsyncRepository<>), typeof(SecurityAsyncRepository<>));

            return services;
        }
    }
}
