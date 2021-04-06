using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingBookApi.Models
{
    public class Recipe : Base
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CookingTime { get; set; }
        public int PrepTime { get; set; }
        public bool IsPublished { get; set; }
        public ICollection<Ingredient> Ingredients { get; set;}
    }
}