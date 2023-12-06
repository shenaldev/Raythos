using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raythos.Models
{
    public class Address
    {
        [Key]
        public Int64 AddressID { get; set; }

        [Required]
        [StringLength(100)]
        public required string Street { get; set; }

        [Required]
        [StringLength(100)]
        public required string City { get; set; }

        [Required]
        [StringLength(20)]
        public required string PostalCode { get; set; }

        [Required]
        [ForeignKey("Country")]
        public long CountryId { get; set; }
        public required Country Country { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }
        public required User User { get; set; }
    }
}
