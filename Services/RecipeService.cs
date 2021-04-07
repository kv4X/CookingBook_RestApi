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
        public async Task<RecipesResponsePagination> Get(RecipeQueryParams recipeParams){
            //Console.WriteLine($"page limit: {recipeParams.limit}!");
            //Console.WriteLine($"page: {recipeParams.page}!");

            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .AsNoTracking()
                .ToListAsync();

            // page
            recipeParams.Page = (recipeParams.Page <= 0)? 1: recipeParams.Page;
            recipeParams.Limit = (recipeParams.Limit <= 0)? 10: recipeParams.Limit;
            var countRecipes = recipes.Count();
            var pageCount = (int)recipes.Count() / recipeParams.Limit;
            
            //Console.WriteLine($"broj stranica: {pageCount}!");
            var startRow = (recipeParams.Page - 1) * recipeParams.Limit;

            // sorting
            switch (recipeParams.SortBy){
                case "title_asc":
                    recipes = recipes.OrderBy(s => s.Title).ToList();
                    break;
                case "title_desc":  
                    recipes = recipes.OrderByDescending(s => s.Title).ToList();
                    break;
                case "date_asc":
                    recipes = recipes.OrderBy(s => s.CreatedDate).ToList();
                    break;
                case "date_desc":
                    recipes = recipes.OrderByDescending(s => s.CreatedDate).ToList();
                    break;
            }
            
            var recipesPaged = recipes.Skip(startRow).Take(recipeParams.Limit);
            return new RecipesResponsePagination {
                CurrentPage = recipeParams.Page,
                TotalPages = pageCount,
                TotalItems = countRecipes,
                Results = _mapper.Map<IEnumerable<RecipeDto>>(recipesPaged)
            };
            
            
            // rucno bez automappera 
            /*  
            return new RecipesResponsePagination {
                CurrentPage = recipeParams.page,
                TotalPages = pageCount,
                TotalItems = countRecipes,
                Results = recipesPaged.Select(p => new RecipeDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CookingTime = p.CookingTime,
                    PrepTime = p.PrepTime, 
                    IsPublished = p.IsPublished,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    Ingredients = p.Ingredients.Select(i => new IngredientDto{
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description
                    })
                }).ToList()
            };
            */
            
        }

        public async Task<IEnumerable<RecipeDto>> GetAll(){
            //var recipes = await _context.Recipes.ToListAsync();
            //return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
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