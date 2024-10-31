using CookBook.Abstractions;
using CookBook.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<IngredientVm>>> GetAllIngredients()
        {
            return Ok(await _ingredientRepository.GetAllIngredientsAsync());
        }
    }
}
