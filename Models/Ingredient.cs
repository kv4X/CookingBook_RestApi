using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingBookApi.Models
{
    public class Ingredient : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column("RecipeId")] 
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe recipe;
    }
}