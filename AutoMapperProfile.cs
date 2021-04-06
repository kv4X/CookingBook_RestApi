using AutoMapper;
using CookingBookApi.Models;
using CookingBookApi.Models.Dtos;
using CookingBookApi.Models.Resources;
namespace CookingBookApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()  
        {  
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<SaveRecipeResource, Recipe>();
            CreateMap<SaveRecipeResource, RecipeDto>();

            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<SaveIngredientResource, Ingredient>();
            CreateMap<SaveIngredientResource, IngredientDto>();
        }  
    }
}