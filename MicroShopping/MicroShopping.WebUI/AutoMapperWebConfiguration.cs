using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MicroShopping.Domain;
using MicroShopping.WebUI.Models;

namespace MicroShopping.WebUI
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => 
            {
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new LancePackageProfile());
                cfg.AddProfile(new BrandProfile());
                cfg.AddProfile(new CategoryProfile());
            });
        }

        public class UserProfile : Profile
        {
            protected override void Configure()
            {
                Mapper.CreateMap<User, UserModel>();
                Mapper.CreateMap<UserModel, User>();
                Mapper.CreateMap<User, ProfileModel>();
                Mapper.CreateMap<ProfileModel, User>();
            }
        }

        public class LancePackageProfile : Profile
        {
            protected override void Configure()
            {
                Mapper.CreateMap<LancePackage, LancePackageModel>();
                Mapper.CreateMap<LancePackageModel, LancePackage>();
            }
        }

        public class BrandProfile : Profile
        {
            protected override void Configure()
            {
                Mapper.CreateMap<ProductBrand, ProductBrandModel>();
                Mapper.CreateMap<ProductBrandModel, ProductBrand>();
            }
        }

        public class CategoryProfile : Profile
        {
            protected override void Configure()
            {
                Mapper.CreateMap<ProductCategory, ProductCategoryModel>();
                Mapper.CreateMap<ProductCategoryModel, ProductCategory>();
            }
        }
    }
}