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

            foreach (var ingredientDto in createRecipeDto.Ingredients)
            {
                Ingredient newIngredient = null;
                var ingredient = await _cookBookDbContext.Ingredients
                    .FirstOrDefaultAsync(i => i.Name.Equals(ingredientDto.Name));
                if (ingredient == null)
                {
                    newIngredient = _mapper.Map<Ingredient>(ingredientDto);
                    newIngredient.Id = GenerateId();
                    newIgredients.Add(newIngredient);
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

        public async Task<ListOfRecipes> GetAllRecipesAsync()
        {
            var recipes = await _cookBookDbContext.Recipes.ToListAsync();
            var listOfRecipes = _mapper.Map<ListOfRecipes>(recipes);
            return listOfRecipes;
        }

        public async Task<RecipeVm> GetRecipeAsync(int id)
        {
            var recipe = await _cookBookDbContext.Recipes
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            // Если рецепт не найден, возвращаем null или выбрасываем исключение
            if (recipe == null)
            {
                return null; // или throw new NotFoundException("Recipe not found");
            }

            // Получаем IngredientVm для всех ингредиентов, связанных с рецептом
            var ingredientVms = await _cookBookDbContext.RecipeIngredients
                .Where(ri => ri.RecipeId == recipe.Id)
                .Select(ri => new
                {
                    IngredientId = ri.IngredientId // Получаем только IngredientId из RecipeIngredients
                })
                .Join(_cookBookDbContext.Ingredients,
                    ri => ri.IngredientId, // Из RecipeIngredients
                    i => i.Id, // Из Ingredients
                    (ri, i) => new IngredientVm(i.Id, i.Name)) // Создаем IngredientVm
                .ToListAsync();

            // Создаем RecipeVm с полученными данными
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
            var oldRecipe = await _cookBookDbContext.Recipes.FirstOrDefaultAsync(recipe => recipe.Id == id);
            var recipe = _mapper.Map<Recipe>(updateRecipeDto);
            if (oldRecipe is null)
            {
                throw new Exception();
            }
            oldRecipe.Name = recipe.Name;
            oldRecipe.Description = recipe.Description;
            oldRecipe.Algorithm = recipe.Algorithm;
            oldRecipe.RecipeCategory = recipe.RecipeCategory;
            await _cookBookDbContext.SaveChangesAsync();
        }
        private static int GenerateId()
        {
            return BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0); // Генерация int из GUID
        }
    }
}
