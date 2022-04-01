using AutoMapper;
using Boilerplate.Application.DTOs.User;
using Boilerplate.Domain.Auth;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Map
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<UpdatePasswordDto, User>();
        }
    }
}
