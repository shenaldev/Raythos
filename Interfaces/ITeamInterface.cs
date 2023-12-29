using Raythos.DTOs;
using Raythos.Responses;

namespace Raythos.Interfaces
{
    public interface ITeamInterface
    {
        ICollection<TeamDto> GetTeams(int skip, int take = 15);

        TeamResponse GetTeam(long id);

        TeamDto CreateTeam(TeamDto team);

        bool UpdateTeam(long id, TeamDto team);

        bool DeleteTeam(long id);

        bool IsTeamExists(long id);

        bool IsTeamExists(string name);

        int GetTotalTeams();
    }
}
