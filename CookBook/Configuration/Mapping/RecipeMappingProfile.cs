using AutoMapper;
using CookBook.Models;
using CookBook.Contracts;
namespace CookBook.Configuration.Mapping
{
    public class RecipeMappingProfile : Profile
    {
        public RecipeMappingProfile()
        {
            CreateMap<Recipe, RecipeVm>();

            CreateMap<Recipe, RecipeListVm>();

            CreateMap<IEnumerable<Recipe>, ListOfRecipes>()
                .ForCtorParam(nameof(ListOfRecipes.Recipes),
                source => source.MapFrom(recipeList => recipeList.ToList()));

            CreateMap<CreateRecipeDto, Recipe>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // добавить логику для ингредиента
                .ForMember(dest => dest.Score, opt => opt.Ignore())
                .ForMember(dest => dest.Name, source => source.MapFrom(s => s.Name.Trim()));
            CreateMap<UpdateRecipeDto, Recipe>()
                .ForMember(dest => dest.Score, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, source => source.MapFrom(s => s.Name.Trim())
                );

        }
    }
}
