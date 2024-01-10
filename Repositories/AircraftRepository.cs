using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Aircrafts;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AircraftRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<AircraftDto>> GetAircrafts(int skip, int take = 15)
        {
            return _mapper.Map<ICollection<AircraftDto>>(
                await _context.Aircrafts
                    .Include(a => a.Category)
                    .Skip(skip)
                    .Take(take)
                    .OrderBy(a => a.Id)
                    .ToListAsync()
            );
        }

        public async Task<AircraftSingleDto?> GetAircraft(long id)
        {
            return _mapper.Map<AircraftSingleDto>(
                await _context.Aircrafts
                    .Where(a => a.Id == id)
                    .Include(a => a.Category)
                    .Include(a => a.Team)
                    .Include(a => a.AircraftOptions)
                    .FirstOrDefaultAsync()
            );
        }

        public async Task<AircraftPostDto?> CreateAircraft(AircraftPostDto aircraft)
        {
            try
            {
                Aircraft newAircraft = _mapper.Map<Aircraft>(aircraft);
                newAircraft.CreatedAt = DateTime.Now;
                newAircraft.UpdatedAt = DateTime.Now;

                await _context.Aircrafts.AddAsync(newAircraft);
                int isSaved = await _context.SaveChangesAsync();
                if (isSaved > 0)
                {
                    return _mapper.Map<AircraftPostDto>(newAircraft);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AircraftDto?> UpdateAircraft(long id, AircraftDto aircraft)
        {
            try
            {
                Aircraft updateAircraft = _mapper.Map<Aircraft>(aircraft);
                updateAircraft.Id = id;
                updateAircraft.UpdatedAt = DateTime.Now;

                _context.Entry(updateAircraft).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _mapper.Map<AircraftDto>(updateAircraft);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAircraftStatus(long id, string status)
        {
            try
            {
                Aircraft? aircraft = await _context.Aircrafts.FindAsync(id);
                if (aircraft == null)
                    return false;

                aircraft.Status = status;
                aircraft.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAircraft(long id)
        {
            try
            {
                Aircraft? aircraft = await _context.Aircrafts.FindAsync(id);
                if (aircraft == null)
                    return false;

                _context.Aircrafts.Remove(aircraft);
                int isSaved = await _context.SaveChangesAsync();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsAircraftExists(long id)
        {
            return await _context.Aircrafts.AnyAsync(a => a.Id == id);
        }

        public async Task<int> GetTotalAircrafts()
        {
            return await _context.Aircrafts.CountAsync();
        }

        public async Task<bool> IsTeamAssigned(long teamId)
        {
            return await _context.Aircrafts.AnyAsync(a => a.TeamId == teamId);
        }
    }
}
