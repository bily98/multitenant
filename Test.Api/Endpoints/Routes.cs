namespace Test.Api.Endpoints
{
    public class Routes
    {
        public const string Base = "/api";

        public static class Auth
        {
            public const string Login = Base + "/auth/login";
            public const string RefreshToken = Base + "/auth/refresh-token";
        }

        public static class Tenants
        {
            public const string Get = Base + "/tenants";
            public const string GetById = Base + "/tenants/{Id:int}";
            public const string Create = Base + "/tenants";
            public const string Update = Base + "/tenants/{Id:int}";
            public const string Delete = Base + "/tenants/{Id:int}";
        }

        public static class Products
        {
            public const string Get = Base + "/{SlugTenant}/products";
            public const string GetById = Base + "/{SlugTenant}/products/{Id:int}";
            public const string Create = Base + "/{SlugTenant}/products";
            public const string Update = Base + "/{SlugTenant}/products/{Id:int}";
            public const string Delete = Base + "/{SlugTenant}/products/{Id:int}";
        }

        public static class Users
        {
            public const string Get = Base + "/{SlugTenant}/users";
            public const string GetById = Base + "/{SlugTenant}/users/{Id:int}";
            public const string Create = Base + "/{SlugTenant}/users";
            public const string Update = Base + "/{SlugTenant}/users/{Id:int}";
            public const string Delete = Base + "/{SlugTenant}/users/{Id:int}";
        }
    }
}
