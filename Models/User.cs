using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(15)]
        public string ContactNo { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
