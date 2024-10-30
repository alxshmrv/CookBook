using AutoMapper;
using CookBook.Models;
using CookBook.Contracts;
using CookBook.CookBook_Database;
namespace CookBook.Configuration.Mapping
{
    public class RecipeMappingProfile : Profile
    {

        public RecipeMappingProfile()
        {
            CreateMap<Recipe, RecipeListVm>();

            CreateMap<IEnumerable<Recipe>, ListOfRecipes>()
                .ForCtorParam(nameof(ListOfRecipes.Recipes),
                source => source.MapFrom(recipeList => recipeList.ToList()));

            CreateMap<UpdateRecipeDto, Recipe>()
                .ForMember(dest => dest.Score, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, source => source.MapFrom(s => s.Name.Trim())
                );

        }
    }
}
