using Raythos.DTOs;

namespace Raythos.Interfaces
{
    public interface IAircraftInterface
    {
        ICollection<AircraftDto> GetAircrafts();

        AircraftDto GetAircraft(long id);

        AircraftDto CreateAircraft(AircraftDto aircraft);

        bool UpdateAircraft(long id, AircraftDto aircraft);

        bool DeleteAircraft(long id);

        bool IsAircraftExists(long id);
    }
}
