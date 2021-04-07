using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBookApi.Services;
using CookingBookApi.Models.Dtos;
using CookingBookApi.Models.Resources;
namespace CookingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService){
            _recipeService = recipeService;
        }

/*
        [HttpGet]
        public async Task<IActionResult> Get(){
            var recipes = await _recipeService.Get();
            return Ok(recipes);
        }

        */

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RecipeQueryParams recipeParams){

            var recipes = await _recipeService.Get(recipeParams);
            return Ok(recipes);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> Get(int Id){
            var recipe = await _recipeService.Get(Id);
            if(recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPut]
        [Route("{Id}")]
         public async Task<IActionResult> Update(int Id, [FromBody] SaveRecipeResource resource){
            if (!ModelState.IsValid) return BadRequest();
            var recipe = await _recipeService.Update(Id, resource);
            if(recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] SaveRecipeResource resource)
        {
            if (!ModelState.IsValid) return BadRequest();
            var recipe = await _recipeService.Insert(resource);
            return Ok(recipe);
        
        }
    }
}