using CookBook.Models;

namespace CookBook.Contracts
{
        public record RecipeVm(int Id, string Name, string Description);
        public record RecipeListVm(int Id, string Name);
        public record ListOfRecipes(List<RecipeListVm> Recipes);
        public record CreateRecipeDto(string Name, string Description, string Algorithm, RecipeCategory RecipeCategory, List<IngredientDto> Ingredients); 
        public record UpdateRecipeDto(string Name, string Description, string Algorithm, RecipeCategory RecipeCategory);


}
