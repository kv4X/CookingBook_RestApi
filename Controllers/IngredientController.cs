using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBookApi.Services;
using CookingBookApi.Models.Dtos;
using CookingBookApi.Models.Resources;
namespace CookingBookApi.Controllers
{
    [Route("api/recipe/")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService){
            _ingredientService = ingredientService;
        }

        [HttpPost]
        [Route("{recipeId}/ingredient")]
        public async Task<IActionResult> Insert(int recipeId, [FromBody] SaveIngredientResource resource)
        {
            if (!ModelState.IsValid) return BadRequest();
            var ingredient = await _ingredientService.Insert(recipeId, resource);
            if(ingredient == null) return NotFound();
            return Ok(ingredient);
        }

        [HttpGet]
        [Route("{recipeId}/ingredient/{ingredientId}")]
        public async Task<IActionResult> Get(int recipeId, int ingredientId){
            var ingredient = await _ingredientService.Get(recipeId, ingredientId);
            if(ingredient == null) return NotFound();
            return Ok(ingredient);
        }

        [HttpPut]
        [Route("{recipeId}/ingredient/{ingredientId}")]
         public async Task<IActionResult> Update(int recipeId, int ingredientId, [FromBody] SaveIngredientResource resource){
            if (!ModelState.IsValid) return BadRequest();
            var ingredientExist = await _ingredientService.Get(recipeId, ingredientId);
            if(ingredientExist == null) return NotFound();
            
            var ingredient = await _ingredientService.Update(recipeId, ingredientId, resource);
            return Ok(ingredient);
        }
    }
}