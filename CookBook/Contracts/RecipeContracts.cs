using CookBook.Models;

namespace CookBook.Contracts
{
    public class RecipeVm()
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IngredientVm> IngredientVms { get; set; }
    }

    public record RecipeListVm(int Id, string Name);
    public record ListOfRecipes(List<RecipeListVm> Recipes);
    public record CreateRecipeDto(string Name, string Description, string Algorithm, RecipeCategory RecipeCategory, List<IngredientDto> Ingredients);
    public record UpdateRecipeDto(string Name, string Description, string Algorithm, RecipeCategory RecipeCategory, List<IngredientDto> Ingredients);


}
