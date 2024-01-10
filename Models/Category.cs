using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Raythos.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Slug { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public ICollection<Aircraft> Aircrafts { get; set; } = new List<Aircraft>();
    }
}
