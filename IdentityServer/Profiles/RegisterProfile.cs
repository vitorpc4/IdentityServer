using AutoMapper;
using IdentityServer.Data.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Profiles
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, IdentityUser<int>>();
            CreateMap<User, CustomIdentityUser>();
        }
    }
}
