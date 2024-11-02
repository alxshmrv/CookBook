using AutoMapper;
using CookBook.Models;
using CookBook.Contracts;
using CookBook.CookBook_Database;

namespace CookBook.Configuration.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, source => source.Ignore())
                .ForMember(dest => dest.Recipes, source => source.Ignore())
                .ForMember(dest => dest.Name, source => source.MapFrom(s => s.Name.Trim()));
        }
    }
}
