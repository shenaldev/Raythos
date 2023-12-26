using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raythos.Models;
using Raythos.Responses;
using System.Text.Json.Nodes;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/dashboard/admin/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JsonObject>>> GetUsers([FromQuery] int page = 1)
        {
            var take = 15;
            var skip = (page - 1) * take;

            var user = await _context.Users
                .Select(
                    u =>
                        new
                        {
                            u.Id,
                            u.FName,
                            u.LName,
                            u.Email,
                            u.ContactNo,
                            u.CreatedAt
                        }
                )
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            int total = await _context.Users.CountAsync();

            PaginatedResponse response =
                new()
                {
                    Data = user,
                    Meta = new Meta
                    {
                        Total = total,
                        Page = page,
                        LastPage = (int)Math.Ceiling((double)total / take)
                    }
                };

            return Ok(response);
        }

        // GET: api/dashboard/admin/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/dashboard/admin/user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(long id, [FromForm] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            User currentUser = await _context.Users.FindAsync(id);
            currentUser.FName = user.FName;
            currentUser.LName = user.LName;
            currentUser.ContactNo = user.ContactNo;
            currentUser.UpdatedAt = DateTime.Now;

            _context.Entry(currentUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/dashboard/admin/user
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromForm] User user)
        {
            //CHECK IF USER ALREADY EXISTS
            if (user.Email != null)
            {
                var isUserExits = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (isUserExits != null)
                {
                    return BadRequest("User already exists");
                }
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/dashboard/admin/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User Has Deleted");
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
