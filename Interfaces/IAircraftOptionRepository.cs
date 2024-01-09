using Raythos.DTOs;

namespace Raythos.Interfaces
{
    public interface IAircraftOptionRepository
    {
        Task<ICollection<AircraftOptionDto>> GetAircraftCustomizations(int skip, int take = 15);

        Task<AircraftOptionDto?> GetAircraftCustomization(long id);

        Task<ICollection<AircraftOptionDto>> GetAircraftCustomizationByAircraftId(long aircraftId);

        Task<AircraftOptionDto?> AddCustomization(AircraftOptionDto aircraftCustomization);

        Task<AircraftOptionDto?> UpdateCustomization(AircraftOptionDto aircraftCustomization);

        Task<bool> DeleteCustomization(long id);

        Task<bool> IsCustomizationExists(long id);

        Task<int> GetTotalCustomizations();
    }
}
