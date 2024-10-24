using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ListOfRecipes>> GetAllRecipes()
        {
            var listOfRecipes = await _recipeRepository.GetAllRecipesAsync();
            return listOfRecipes;
        }
        [HttpGet("by_id")]
        public async Task<ActionResult<RecipeVm>> GetRecipeById(int id)
        {
            var recipeVm = await _recipeRepository.GetRecipeAsync(id);
            if (recipeVm == null)
            {
                return NotFound(id);
            }
            return Ok(recipeVm);
        }
        [HttpPost]
        public async Task<ActionResult<int>> AddRecipe(CreateRecipeDto createRecipeDto)
                   => await _recipeRepository.AddRecipeAsync(createRecipeDto);
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecipe(int id, UpdateRecipeDto updateRecipeDto)
        {
            await _recipeRepository.UpdateRecipeAsync(updateRecipeDto, id);
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            await _recipeRepository.DeleteRecipeAsync(id);
            return NoContent();
        }
    }
}
