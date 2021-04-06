using System;
namespace CookingBookApi.Models.Dtos
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
       // public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}