using AutoMapper;
using DatingApp.API.Data.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Extensions;

namespace DatingApp.API.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, MemberDto>()
                .ForMember(dest => dest.Age,
                    options => options
                        .MapFrom(src => src.DateOfBirth.CalculateAge()));
        }
    }
}