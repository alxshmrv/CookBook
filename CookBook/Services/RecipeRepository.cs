using AutoMapper;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.CookBook_Database;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services
{
    public class RecipeRepository : IRecipeRepository
    {

        private readonly CookBookDbContext _cookBookDbContext;
        private readonly IMapper _mapper;
        public RecipeRepository(CookBookDbContext cookBookDbContext, IMapper mapper)
        {
            _cookBookDbContext = cookBookDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddRecipeAsync(CreateRecipeDto createRecipeDto)
        {
            var recipe = new Recipe()
            {
                Id = GenerateId(),
                Name = createRecipeDto.Name,
                Description = createRecipeDto.Description,
                Algorithm = createRecipeDto.Algorithm,
                RecipeCategory = createRecipeDto.RecipeCategory,
                RecipeIngredients = new List<RecipeIngredient>()
            };

            var newIgredients = new List<Ingredient>();

            await AddIngredientsToRecipeAsync(createRecipeDto.Ingredients, recipe, newIgredients);

            await _cookBookDbContext.Recipes.AddAsync(recipe);
            await _cookBookDbContext.Ingredients.AddRangeAsync(newIgredients);
            await _cookBookDbContext.RecipeIngredients.AddRangeAsync(recipe.RecipeIngredients);
            await _cookBookDbContext.SaveChangesAsync();
            return recipe.Id;
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _cookBookDbContext.Recipes.FirstOrDefaultAsync(recipe => recipe.Id == id);
            if (recipe is null)
            {
                throw new Exception();
            }
            _cookBookDbContext.Remove(recipe);
            await _cookBookDbContext.SaveChangesAsync();

        }

        public async Task<ListOfRecipes> GetAllRecipesAsync(List<RecipeCategory> categoryFilter = null)
        {
            var query = _cookBookDbContext.Recipes.AsQueryable();
            if (categoryFilter != null && categoryFilter.Count > 0)
            {
                query = query.Where(r => categoryFilter.Contains(r.RecipeCategory));
            }
            var recipes = await query.ToListAsync();
            var listOfRecipes = _mapper.Map<ListOfRecipes>(recipes);
            return listOfRecipes;
        }

        public async Task<RecipeVm> GetRecipeAsync(int id)
        {
            var recipe = await _cookBookDbContext.Recipes
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return null;
            }

            var ingredientVms = await _cookBookDbContext.RecipeIngredients
                .Where(ri => ri.RecipeId == recipe.Id)
                .Select(ri => new
                {
                    IngredientId = ri.IngredientId
                })
                .Join(_cookBookDbContext.Ingredients,
                    ri => ri.IngredientId,
                    i => i.Id,
                    (ri, i) => new IngredientVm(i.Id, i.Name))
                .ToListAsync();

            var recipeVm = new RecipeVm
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                IngredientVms = ingredientVms
            };
            return recipeVm;
        }

        public async Task UpdateRecipeAsync(UpdateRecipeDto updateRecipeDto, int id)
        {
            var recipe = await _cookBookDbContext.Recipes
       .Include(r => r.RecipeIngredients)
       .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                throw new Exception();
            }

            recipe.Name = updateRecipeDto.Name;
            recipe.Description = updateRecipeDto.Description;
            recipe.Algorithm = updateRecipeDto.Algorithm;
            recipe.RecipeCategory = updateRecipeDto.RecipeCategory;

            _cookBookDbContext.RecipeIngredients.RemoveRange(recipe.RecipeIngredients);

            var newIgredients = new List<Ingredient>();

            await AddIngredientsToRecipeAsync(updateRecipeDto.Ingredients, recipe, newIgredients);

            if (newIgredients != null)
            {
            await _cookBookDbContext.Ingredients.AddRangeAsync(newIgredients);
            }

            await _cookBookDbContext.SaveChangesAsync();
        }

        private async Task AddIngredientsToRecipeAsync(ICollection<IngredientDto> ingredients, Recipe recipe, List<Ingredient> newIngredients)
        {
            foreach (var ingredientDto in ingredients)
            {
                Ingredient newIngredient = null;
                var ingredient = await _cookBookDbContext.Ingredients
                    .FirstOrDefaultAsync(i => i.Name.Equals(ingredientDto.Name));

                if (ingredient == null)
                {
                    newIngredient = _mapper.Map<Ingredient>(ingredientDto);
                    newIngredient.Id = GenerateId();
                    newIngredients.Add(newIngredient);
                }

                var recipeIngredient = new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    Recipe = recipe,
                    IngredientId = ingredient?.Id ?? newIngredient.Id,
                    Ingredient = ingredient ?? newIngredient,
                    Quantity = ingredientDto.Quantity,
                    Unit = ingredientDto.Unit
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
        }

        private static int GenerateId()
        {
            return BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0); // Генерация int из GUID
        }
    }
}
