using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Raythos.Models;
using Raythos.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Raythos.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration Configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            if (email == null || password == null)
            {
                return BadRequest("Enter Email And Password");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return BadRequest("Invalid Credientials");
            }

            //CHECK IF PASSWORD IS CORRECT
            try
            {
                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return Unauthorized();
            }

            //SEND JSON WEB TOKEN
            var token = GenerateJwt(user, Configuration);
            var response = new LoginResponse
            {
                UserID = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Token = token
            };
            return Ok(response);
        }

        //POST: api/auth/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            //CHECK IF USER ALREADY EXISTS
            if (user.Email != null)
            {
                var isUserExits = await _context.Users.FirstOrDefaultAsync(
                    u => u.Email == user.Email
                );
                if (isUserExits != null)
                {
                    return BadRequest("User already exists");
                }
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            string token = GenerateJwt(user, Configuration);
            var response = new LoginResponse
            {
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Token = token
            };

            return Ok(response);
        }

        /***
         * GENERATE JWT TOKEN
         * *********/
        private string GenerateJwt(User user, IConfiguration config)
        {
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);

            // Specify signing and encryption algorithms
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                    }
                ),
                Expires = DateTime.Now.AddMinutes(15000),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            // Generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
