using AutoMapper;
using Test.Api.Endpoints.Products;
using Test.Api.Endpoints.Tenants;
using Test.Api.Endpoints.Users;
using Test.Core.Entities;

namespace Test.Api.MappingsProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Tenant
            CreateMap<Tenant, GetAllTenantResponse>();

            CreateMap<Tenant, GetByIdTenantResponse>();

            CreateMap<PostTenantRequest, Tenant>().
                ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Name.ToLower().Replace(" ", "_")));

            CreateMap<Tenant, PostTenantResponse>();

            CreateMap<UpdateTenantRequestBody, Tenant>();
            CreateMap<Tenant, UpdateTenantResponse>();
            #endregion

            #region User
            CreateMap<User, GetAllUserResponse>();

            CreateMap<User, GetByIdUserResponse>();

            CreateMap<PostUserRequestBody, User>();
            CreateMap<User, PostUserResponse>();

            CreateMap<UpdateUserRequestBody, User>();
            CreateMap<User, UpdateUserResponse>();
            #endregion

            #region Product
            CreateMap<Product, GetAllProductResponse>();

            CreateMap<Product, GetByIdProductResponse>();

            CreateMap<PostProductRequestBody, Product>();
            CreateMap<Product, PostProductResponse>();

            CreateMap<UpdateProductRequestBody, Product>();
            CreateMap<Product, UpdateProductResponse>();
            #endregion
        }
    }
}
