using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions
{
    public interface IIngredientRepository
    {
        Task<List<IngredientVm>> GetAllIngredientsAsync();
    }
}
