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
        }
    }
}
