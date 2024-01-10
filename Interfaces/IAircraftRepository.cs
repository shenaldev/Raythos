using Raythos.DTOs.Aircrafts;

namespace Raythos.Interfaces
{
    public interface IAircraftRepository
    {
        Task<ICollection<AircraftDto>> GetAircrafts(int skip, int take = 15);

        Task<AircraftSingleDto?> GetAircraft(long id);

        Task<AircraftPostDto?> CreateAircraft(AircraftPostDto aircraft);

        Task<AircraftDto?> UpdateAircraft(long id, AircraftDto aircraft);

        Task<bool> UpdateAircraftStatus(long id, string status);

        Task<bool> DeleteAircraft(long id);

        Task<bool> IsAircraftExists(long id);

        Task<int> GetTotalAircrafts();

        Task<bool> IsTeamAssigned(long teamId);
    }
}
