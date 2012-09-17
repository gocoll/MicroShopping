﻿using System;
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
    }
}