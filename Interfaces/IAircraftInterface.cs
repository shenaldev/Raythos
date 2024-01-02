using Raythos.DTOs.Aircrafts;

namespace Raythos.Interfaces
{
    public interface IAircraftInterface
    {
        ICollection<AircraftDto> GetAircrafts(int skip, int take = 15);

        AircraftDto GetAircraft(long id);

        AircraftPostDto CreateAircraft(AircraftPostDto aircraft);

        bool UpdateAircraft(long id, AircraftDto aircraft);

        bool DeleteAircraft(long id);

        bool IsAircraftExists(long id);

        int GetTotalAircrafts();

        bool IsTeamAssigned(long teamId);
    }
}
