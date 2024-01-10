using Microsoft.EntityFrameworkCore;
using Raythos.Interfaces;

namespace Raythos.Repositories
{
    public class ForignkeyCheckRepository : IForignkeyRepository
    {
        private readonly ApplicationDbContext _context;

        public ForignkeyCheckRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAircraftExists(long id)
        {
            return await _context.Aircrafts.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsCartExists(long id)
        {
            return await _context.Carts.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsCartExists(long userId, long aircraftId)
        {
            return await _context.Carts.AnyAsync(
                e => e.UserId == userId && e.AircraftId == aircraftId
            );
        }

        public async Task<bool> IsTeamExists(long id)
        {
            return await _context.Teams.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsUserExists(long id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsAddressExists(long id)
        {
            return await _context.Addresses.AnyAsync(e => e.AddressID == id);
        }
    }
}
