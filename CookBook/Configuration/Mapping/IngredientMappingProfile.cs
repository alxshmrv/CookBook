using AutoMapper;
using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Configuration.Mapping
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            CreateMap<IngredientDto, Ingredient>()
                .ForMember(dest => dest.Id, source => source.Ignore())
                .ForMember(dest => dest.RecipeIngredients, source => source.Ignore())
                .ForMember(dest => dest.Name, source => source.MapFrom(s => s.Name.Trim()));
        }
    }
}
