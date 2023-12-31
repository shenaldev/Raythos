using Raythos.DTOs;

namespace Raythos.Interfaces
{
    public interface IAircraftInterface
    {
        ICollection<AircraftDto> GetAircrafts(int skip, int take = 15);

        AircraftDto GetAircraft(long id);

        AircraftDto CreateAircraft(AircraftDto aircraft);

        bool UpdateAircraft(long id, AircraftDto aircraft);

        bool DeleteAircraft(long id);

        bool IsAircraftExists(long id);

        int GetTotalAircrafts();
    }
}
