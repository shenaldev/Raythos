using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs.InventoryDtos
{
    public class UpdateInventoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = null!;

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public decimal? UnitPrice { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
