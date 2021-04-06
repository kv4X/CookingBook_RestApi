using System;
using System.Collections.Generic;
namespace CookingBookApi.Models.Dtos
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CookingTime { get; set; }
        public int PrepTime { get; set; }
        public bool IsPublished { get; set; }
        public IEnumerable<IngredientDto> Ingredients { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }   
    }
}