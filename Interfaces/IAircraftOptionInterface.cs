using Raythos.DTOs;

namespace Raythos.Interfaces
{
    public interface IAircraftOptionInterface
    {
        ICollection<AircraftOptionDto> GetAircraftCustomizations(int skip, int take = 15);

        AircraftOptionDto GetAircraftCustomization(long id);

        ICollection<AircraftOptionDto> GetAircraftCustomizationByAircraftId(long aircraftId);

        AircraftOptionDto AddCustomization(AircraftOptionDto aircraftCustomization);

        bool UpdateCustomization(AircraftOptionDto aircraftCustomization);

        bool DeleteCustomization(long id);

        bool IsCustomizationExists(long id);

        int GetTotalCustomizations();
    }
}
