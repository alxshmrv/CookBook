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
            var recipe = _mapper.Map<Recipe>(createRecipeDto);
            await _cookBookDbContext.Recipes.AddAsync(recipe);
            await _cookBookDbContext.SaveChangesAsync();

            var recipeIngredients = new List<RecipeIngredient>();
            foreach (var ingredientDto in createRecipeDto.Ingredients)
            {
                Ingredient newIngredient = null;
                var ingredient = await _cookBookDbContext.Ingredients
                    .FirstOrDefaultAsync(i => i.Name.Equals(ingredientDto.Name));
                if (ingredient == null)
                {
                    newIngredient = _mapper.Map<Ingredient>(ingredientDto);
                    await _cookBookDbContext.Ingredients.AddAsync(newIngredient);
                    await _cookBookDbContext.SaveChangesAsync();
                }
                var recipeIngredient = new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = ingredient?.Id ?? newIngredient.Id,
                    Quantity = ingredientDto.Quantity,
                    Unit = ingredientDto.Unit
                };
                recipeIngredients.Add(recipeIngredient);
            }
            await _cookBookDbContext.RecipeIngredients.AddRangeAsync(recipeIngredients);
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
            var recipe = await _cookBookDbContext.Recipes.FirstOrDefaultAsync(x => x.Id == id);
            var recipeVm = _mapper.Map<RecipeVm>(recipe);
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
    }
}
