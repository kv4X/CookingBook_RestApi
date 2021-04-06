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
using System;

namespace CookingBookApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;
        protected readonly CBDbContext _context;

        public RecipeService(CBDbContext context, IMapper maper){
            _context = context; 
            _mapper = maper;
        }
        public async Task<IEnumerable<RecipeDto>> Get(){
            /*var recipes = await _context.Recipes.ToListAsync();
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);*/
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }
/*
SYNC no no
        public IList<RecipeDto> Get(){
            return _mapper.Map<IList<RecipeDto>>(_context.Recipes.ToList());
        }*/

        public async Task<RecipeDto> Get(int Id){
            /*var recipe = await _context.Recipes.FirstOrDefaultAsync(a => a.Id == Id);
            return _mapper.Map<RecipeDto>(recipe);*/
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Id);
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<RecipeDto> Insert(SaveRecipeResource resource){
            var recipe = _mapper.Map<Recipe>(resource);
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<RecipeDto> Update(int Id, SaveRecipeResource resource){
            var recipeExist = await _context.Recipes.FirstOrDefaultAsync(a => a.Id == Id);
            if(recipeExist == null) return null;
            var recipeReq = _mapper.Map<Recipe>(resource);

            recipeExist.Title = recipeReq.Title;
            recipeExist.Description = recipeReq.Description;
            recipeExist.CookingTime = recipeReq.CookingTime;
            recipeExist.PrepTime = recipeReq.PrepTime;
            recipeExist.IsPublished = recipeReq.IsPublished;

            _context.Recipes.Update(recipeExist);
            await _context.SaveChangesAsync();

            var recipeUpdated = await _context.Recipes.FirstOrDefaultAsync(a => a.Id == Id);
            return _mapper.Map<RecipeDto>(recipeUpdated);
        }
    }
}