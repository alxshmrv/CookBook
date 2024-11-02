using CookBook.Abstractions;
using CookBook.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : BaseController
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<IngredientVm>>> GetAllIngredients()
        {
            return Ok(await _ingredientRepository.GetAllIngredientsAsync());
        }
    }
}
