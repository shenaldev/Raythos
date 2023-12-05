using System.ComponentModel.DataAnnotations;

namespace Raythos.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(150)]
        public string FName { get; set; }

        [MaxLength(150)]
        public string LName { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
