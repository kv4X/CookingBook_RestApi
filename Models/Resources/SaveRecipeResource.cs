using System.ComponentModel.DataAnnotations;
namespace CookingBookApi.Models.Resources
{
    public class SaveRecipeResource
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int CookingTime { get; set; }

        [Required]
        public int PrepTime { get; set; }

        [Required]
        public bool IsPublished { get; set; }
    }
}