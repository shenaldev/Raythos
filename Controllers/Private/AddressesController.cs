using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raythos.Models;
using Raythos.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Raythos.Controllers.Private
{
    [Route("api/user/addresses")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class AddressesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/user/addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            Int64 userId = await GetUserID();
            if (userId != -1)
            {
                return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
            }

            return NotFound();
        }

        // GET: api/user/addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(long id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/user/addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(long id, Address address)
        {
            if (id != address.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/user/addresses
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress([FromForm] Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressID }, address);
        }

        // DELETE: api/user/addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(long id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(long id)
        {
            return _context.Addresses.Any(e => e.AddressID == id);
        }

        //GET USER ID FROM DB
        private async Task<Int64> GetUserID()
        {
            string? userEmail = JWTHelper.GetEmailFromJWT(User);

            if (userEmail != null)
            {
                Int64 userId = await _context.Users
                    .Where(u => u.Email == userEmail)
                    .Select(u => u.Id)
                    .FirstOrDefaultAsync();
            }

            return -1;
        }
    }
}
