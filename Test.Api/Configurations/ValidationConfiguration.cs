using FluentValidation;
using Test.Api.Endpoints.Auth;
using Test.Api.Endpoints.Products;
using Test.Api.Endpoints.Tenants;
using Test.Api.Endpoints.Users;

namespace Test.Api.Configurations
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddValidationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginRequest>, LoginRequestValidation>();

            services.AddScoped<IValidator<PostProductRequestBody>, PostProductRequestBodyValidation>();
            services.AddScoped<IValidator<UpdateProductRequestBody>, UpdateProductRequestBodyValidation>();

            services.AddScoped<IValidator<PostTenantRequest>, PostTenantRequestValidation>();
            services.AddScoped<IValidator<UpdateTenantRequestBody>, UpdateTenantRequestBodyValidation>();

            services.AddScoped<IValidator<PostUserRequestBody>, PostUserRequestBodyValidation>();
            services.AddScoped<IValidator<UpdateUserRequestBody>, UpdateUserRequestBodyValidation>();

            return services;
        }
    }
}
