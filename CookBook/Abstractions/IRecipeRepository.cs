using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions
{
    public interface IRecipeRepository
    {
        Task<ListOfRecipes> GetAllRecipesAsync();
        Task<RecipeVm> GetRecipeAsync(int id);
        Task<int> AddRecipeAsync(CreateRecipeDto createRecipeDto);
        Task UpdateRecipeAsync(UpdateRecipeDto updateRecipeDto, int Id);
        Task DeleteRecipeAsync (int id);
    }
}
