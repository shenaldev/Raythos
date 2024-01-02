using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class AircraftOptionRepository : IAircraftOptionInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AircraftOptionRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<AircraftOptionDto> GetAircraftCustomizations(int skip, int take = 15)
        {
            return _mapper.Map<ICollection<AircraftOptionDto>>(
                _context.AircraftCustomizations.Skip(skip).Take(take).OrderBy(a => a.Id).ToList()
            );
        }

        public AircraftOptionDto GetAircraftCustomization(long id)
        {
            return _mapper.Map<AircraftOptionDto>(
                _context.AircraftCustomizations.Where(a => a.Id == id).FirstOrDefault()
            );
        }

        public ICollection<AircraftOptionDto> GetAircraftCustomizationByAircraftId(long AircraftId)
        {
            return _mapper.Map<ICollection<AircraftOptionDto>>(
                _context.AircraftCustomizations.Where(a => a.AircraftId == AircraftId).ToList()
            );
        }

        public AircraftOptionDto AddCustomization(AircraftOptionDto aircraftCustomization)
        {
            try
            {
                AircraftOption customization = _mapper.Map<AircraftOption>(aircraftCustomization);
                _context.AircraftCustomizations.Add(customization);
                int isSaved = _context.SaveChanges();
                if (isSaved > 0)
                {
                    return _mapper.Map<AircraftOptionDto>(customization);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCustomization(AircraftOptionDto aircraftCustomization)
        {
            try
            {
                AircraftOption updateCustomization = _mapper.Map<AircraftOption>(
                    aircraftCustomization
                );
                _context.Entry(updateCustomization).State = EntityState.Modified;
                int isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomization(long id)
        {
            try
            {
                AircraftOption customization = _context.AircraftCustomizations.Find(id);
                _context.AircraftCustomizations.Remove(customization);
                int isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool IsCustomizationExists(long id)
        {
            return _context.AircraftCustomizations.Any(a => a.Id == id);
        }

        public int GetTotalCustomizations()
        {
            return _context.AircraftCustomizations.Count();
        }
    }
}
