using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class AircraftOptionRepository : IAircraftOptionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AircraftOptionRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<AircraftOptionDto>> GetAircraftCustomizations(
            int skip,
            int take = 15
        )
        {
            return _mapper.Map<ICollection<AircraftOptionDto>>(
                await _context.AircraftOptions
                    .Skip(skip)
                    .Take(take)
                    .OrderBy(a => a.Id)
                    .ToListAsync()
            );
        }

        public async Task<AircraftOptionDto?> GetAircraftCustomization(long id)
        {
            return _mapper.Map<AircraftOptionDto>(
                await _context.AircraftOptions.Where(a => a.Id == id).FirstOrDefaultAsync()
            );
        }

        public async Task<ICollection<AircraftOptionDto>> GetAircraftCustomizationByAircraftId(
            long AircraftId
        )
        {
            return _mapper.Map<ICollection<AircraftOptionDto>>(
                await _context.AircraftOptions.Where(a => a.AircraftId == AircraftId).ToListAsync()
            );
        }

        public async Task<AircraftOptionDto?> AddCustomization(
            AircraftOptionDto aircraftCustomization
        )
        {
            try
            {
                AircraftOption customization = _mapper.Map<AircraftOption>(aircraftCustomization);
                await _context.AircraftOptions.AddAsync(customization);
                int isSaved = await _context.SaveChangesAsync();
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

        public async Task<AircraftOptionDto?> UpdateCustomization(
            AircraftOptionDto aircraftCustomization
        )
        {
            try
            {
                AircraftOption updateCustomization = _mapper.Map<AircraftOption>(
                    aircraftCustomization
                );
                _context.Entry(updateCustomization).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _mapper.Map<AircraftOptionDto>(updateCustomization);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteCustomization(long id)
        {
            try
            {
                AircraftOption? customization = await _context.AircraftOptions.FindAsync(id);
                if (customization == null)
                    return false;

                _context.AircraftOptions.Remove(customization);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsCustomizationExists(long id)
        {
            return await _context.AircraftOptions.AnyAsync(a => a.Id == id);
        }

        public async Task<int> GetTotalCustomizations()
        {
            return await _context.AircraftOptions.CountAsync();
        }
    }
}
