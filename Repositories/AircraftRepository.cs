using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Aircrafts;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class AircraftRepository : IAircraftInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AircraftRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<AircraftDto> GetAircrafts(int skip, int take = 15)
        {
            return _mapper.Map<ICollection<AircraftDto>>(
                _context.Aircrafts.Skip(skip).Take(take).OrderBy(a => a.Id).ToList()
            );
        }

        public AircraftSingleDto GetAircraft(long id)
        {
            return _mapper.Map<AircraftSingleDto>(
                _context.Aircrafts
                    .Where(a => a.Id == id)
                    .Include(a => a.Team)
                    .Include(a => a.AircraftOptions)
                    .FirstOrDefault()
            );
        }

        public AircraftPostDto CreateAircraft(AircraftPostDto aircraft)
        {
            try
            {
                Aircraft newAircraft = _mapper.Map<Aircraft>(aircraft);
                newAircraft.CreatedAt = DateTime.Now;
                newAircraft.UpdatedAt = DateTime.Now;

                _context.Aircrafts.Add(newAircraft);
                int isSaved = _context.SaveChanges();
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

        public bool UpdateAircraft(long id, AircraftDto aircraft)
        {
            try
            {
                Aircraft updateAircraft = _mapper.Map<Aircraft>(aircraft);
                updateAircraft.Id = id;
                updateAircraft.UpdatedAt = DateTime.Now;

                _context.Entry(updateAircraft).State = EntityState.Modified;
                int isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAircraft(long id)
        {
            try
            {
                Aircraft? aircraft = _context.Aircrafts.Find(id);
                if (aircraft == null)
                    return false;

                _context.Aircrafts.Remove(aircraft);
                int isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool IsAircraftExists(long id)
        {
            return _context.Aircrafts.Any(a => a.Id == id);
        }

        public int GetTotalAircrafts()
        {
            return _context.Aircrafts.Count();
        }

        public bool IsTeamAssigned(long teamId)
        {
            return _context.Aircrafts.Any(a => a.TeamId == teamId);
        }
    }
}
