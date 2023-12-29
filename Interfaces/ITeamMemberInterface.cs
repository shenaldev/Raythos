using Raythos.DTOs;

namespace Raythos.Interfaces
{
    public interface ITeamMemberInterface
    {
        ICollection<TeamMemberDto> GetTeamMembers(long teamId);

        TeamMemberDto CreateTeamMember(long teamId, TeamMemberDto teamMember);

        bool DeleteTeamMember(long teamId, long userID);

        bool IsTeamMemberExists(long teamId, long userID);
    }
}
