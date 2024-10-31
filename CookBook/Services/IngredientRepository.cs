using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.CookBook_Database;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly CookBookDbContext _cookBookDbContext;
        public IngredientRepository(CookBookDbContext cookBookDbContext)
        {
            _cookBookDbContext = cookBookDbContext;
        }
        public async Task<List<IngredientVm>> GetAllIngredientsAsync()
        {
            return await _cookBookDbContext.Ingredients
                .Select(i => new IngredientVm(i.Id, i.Name))
                .ToListAsync();
        }
    }
}
