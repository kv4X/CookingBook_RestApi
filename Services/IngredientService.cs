using System.Collections;
using CookingBookApi.Models.Dtos;
using CookingBookApi.Models.Resources;
using CookingBookApi.Models;
using CookingBookApi.Database;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CookingBookApi.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IMapper _mapper;
        private readonly CBDbContext _context;

        public IngredientService(IMapper mapper, CBDbContext context){
            _mapper = mapper;
            _context = context;
        }

        public async Task<IngredientDto> Get(int recipeId, int ingredientId){
            var ingredient = await _context.Ingredients.Where(a => a.Id == ingredientId).Where(a => a.RecipeId == recipeId).FirstOrDefaultAsync();
            if(ingredient == null) return null;
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto> Insert(int recipeId, SaveIngredientResource resource){
            var ingredient = _mapper.Map<Ingredient>(resource);
            var recipe = await _context.Recipes.FirstOrDefaultAsync(a => a.Id == recipeId);
            if(recipe == null) return null;
            recipe.Ingredients = new List<Ingredient>();
            recipe.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return _mapper.Map<IngredientDto>(ingredient);
        }
        public async Task<IngredientDto> Update(int recipeId, int ingredientId, SaveIngredientResource resource){
            var ingredientExist = await _context.Ingredients.Where(a => a.Id == ingredientId).Where(a => a.RecipeId == recipeId).FirstOrDefaultAsync();
            if(ingredientExist == null) return null;
            var ingredientReq = _mapper.Map<Ingredient>(resource);

            ingredientExist.Name = ingredientReq.Name;
            ingredientExist.Description = ingredientReq.Description;
            ingredientExist.RecipeId = ingredientReq.RecipeId;

            _context.Ingredients.Update(ingredientExist);
            await _context.SaveChangesAsync();

            var ingredientUpdated = await _context.Ingredients.FirstOrDefaultAsync(a => a.Id == ingredientId);
            return _mapper.Map<IngredientDto>(ingredientUpdated);
        }
    }
}