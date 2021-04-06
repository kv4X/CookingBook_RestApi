using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBookApi.Models.Dtos;
using CookingBookApi.Models.Resources;
namespace CookingBookApi.Services
{
    public interface IIngredientService
    {   Task<IngredientDto> Insert(int recipeId, SaveIngredientResource resource);
        Task<IngredientDto> Get(int recipeId, int ingredientId);
        Task<IngredientDto> Update(int recipeId, int ingredientId, SaveIngredientResource resource);
        /*Task<IEnumerable<IngredientDto>> Get();
        Task<IngredientDto> Get(int Id);
        
        */
    }
}