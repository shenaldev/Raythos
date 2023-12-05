using System.ComponentModel.DataAnnotations;

namespace Raythos.Models
{
    public class User
    {
        [Key]
        public Int64 Id { get; set; }

        [StringLength(150)]
        [Required]
        public string FName { get; set; }

        [StringLength(150)]
        [Required]
        public string LName { get; set; }

        [StringLength(150)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(15)]
        public string ContactNo { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
