using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raythos.Models;
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
            var take = 1;
            var skip = (page - 1) * take;

            var user = await _context.Users
                .Select(
                    u =>
                        new
                        {
                            u.UserId,
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

            var response = new
            {
                data = user,
                meta = new
                {
                    total,
                    page,
                    last_page = Math.Ceiling((double)total / take)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
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

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
