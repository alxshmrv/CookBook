using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IJwtTokensRepository _jwtTokensRepository;
        public RecipeController(IRecipeRepository recipeRepository,
            IJwtTokenGenerator jwtTokenGenerator, IJwtTokensRepository jwtTokensRepository)
        {
            _jwtTokensRepository = jwtTokensRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _recipeRepository = recipeRepository;
        }
        [AllowAnonymous]
        [HttpGet("all_recipes")]
        public async Task<ActionResult<ListOfRecipes>> GetAllRecipes([FromQuery] List<RecipeCategory> categoryFilter)
        {
            var listOfRecipes = await _recipeRepository.GetAllRecipesAsync(categoryFilter);
            return Ok(listOfRecipes);
        }
        [AllowAnonymous]
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
        [Authorize(Policy = "RecipeOwner")]
        [HttpPost]
        public async Task<ActionResult<int>> AddRecipe(CreateRecipeDto createRecipeDto)
                   => await _recipeRepository.AddRecipeAsync(createRecipeDto);
        [Authorize(Policy = "RecipeOwner")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecipe(int id, UpdateRecipeDto updateRecipeDto)
        {
            await _recipeRepository.UpdateRecipeAsync(updateRecipeDto, id);
            return NoContent();
        }
        [Authorize(Policy = "RecipeOwner")]
        [HttpDelete]
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            await _recipeRepository.DeleteRecipeAsync(id);
            return NoContent();
        }
    }
}
