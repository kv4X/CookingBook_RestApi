using System.Collections.Generic;  
using CookingBookApi.Models.Dtos;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CookingBookApi.Models.Resources;
namespace CookingBookApi.Services
{
    public interface IRecipeService
    {
        Task<RecipesResponsePagination> Get(RecipeQueryParams recipeParams);
        Task<IEnumerable<RecipeDto>> GetAll();

        Task<RecipeDto> Get(int Id);
        Task<RecipeDto> Insert(SaveRecipeResource resource);
        Task<RecipeDto> Update(int Id, SaveRecipeResource resource);
        //void Delete(int Id);  
    }
}