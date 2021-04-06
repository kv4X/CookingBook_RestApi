using System.ComponentModel.DataAnnotations;
namespace CookingBookApi.Models.Resources
{
    public class SaveIngredientResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}