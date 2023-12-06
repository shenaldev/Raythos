using System.ComponentModel.DataAnnotations;

namespace Raythos.Models
{
    public class Country
    {
        [Key]
        public long CountryId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(3)]
        public required string Code { get; set; }
    }
}
