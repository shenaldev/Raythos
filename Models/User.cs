using System.ComponentModel.DataAnnotations;

namespace Raythos.Models
{
    public class User
    {
        [Key]
        public Int64 UserId { get; set; }

        [StringLength(150)]
        [Required]
        public required string FName { get; set; }

        [StringLength(150)]
        [Required]
        public required string LName { get; set; }

        [StringLength(150)]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        [Required]
        [StringLength(200)]
        public required string Password { get; set; }

        [StringLength(15)]
        public required string ContactNo { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Address> Addresses { get; } = new List<Address>();
    }
}
