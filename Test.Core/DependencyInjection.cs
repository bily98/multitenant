using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Core.Interfaces;
using Test.Core.Services;
using Test.SharedKernel.Options;

namespace Test.Core
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add all services to DI container
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Return service collection</returns>
        public static IServiceCollection AddTestCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPasswordService, PasswordService>();

            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

            return services;
        }
    }
}
