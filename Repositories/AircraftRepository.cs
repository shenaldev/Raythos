using AutoMapper;
using Raythos.DTOs;
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

        public ICollection<AircraftDto> GetAircrafts()
        {
            throw new NotImplementedException();
        }

        public AircraftDto GetAircraft(long id)
        {
            throw new NotImplementedException();
        }

        public AircraftDto CreateAircraft(AircraftDto aircraft)
        {
            try
            {
                Aircraft newAircraft = _mapper.Map<Aircraft>(aircraft);
                _context.Aircrafts.Add(newAircraft);
                _context.SaveChanges();
                return aircraft;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateAircraft(long id, AircraftDto aircraft)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAircraft(long id)
        {
            throw new NotImplementedException();
        }

        public bool IsAircraftExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}
