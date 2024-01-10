using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
