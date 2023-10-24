using Ardalis.Result;
using Test.Api.Endpoints;
using Test.Core.Interfaces;
using Test.Infrastructure.Interface;

namespace Test.Api.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        IServiceProvider _serviceProvider;

        public TenantMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.ToString().StartsWith(Routes.Auth.Login) &&
                !context.Request.Path.ToString().StartsWith(Routes.Tenants.Get))
            {
                var route = context.GetRouteData();

                var slugTenant = route.Values["SlugTenant"]?.ToString();

                if (slugTenant == null)
                {
                    context.Response.StatusCode = 400;                 
                    await context.Response.WriteAsync("SlugTenant is missing");
                    return;
                }

                using(var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
                        var tenant = await tenantService.GetBySlugAsync(slugTenant);

                        if (tenant.Status != ResultStatus.Ok)
                        {
                            context.Response.StatusCode = 404;
                            await context.Response.WriteAsync("Tenant not found");
                            return;
                        }

                        var tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
                        tenantProvider.SetTenant(tenant.Value);
                    }
                    catch (Exception e)
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync(e.Message);
                        return;
                    }
                }
            }

            await _next.Invoke(context);
        }
    }
}
