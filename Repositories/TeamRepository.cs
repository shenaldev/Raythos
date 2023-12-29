using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;
using Raythos.Responses;

namespace Raythos.Repositories
{
    public class TeamRepository : ITeamInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get Team List Paginated
        public ICollection<TeamDto> GetTeams(int skip, int take = 15)
        {
            return _mapper.Map<ICollection<TeamDto>>(
                _context.Teams.Skip(skip).Take(take).OrderBy(t => t.Id).ToList()
            );
        }

        // Get Team By Id
        public TeamResponse GetTeam(long id)
        {
            Team? team = _context.Teams
                .Where(t => t.Id == id)
                .Include(t => t.TeamMembers)
                .ThenInclude(tm => tm.User)
                .FirstOrDefault();

            TeamResponse teamResponse = new TeamResponse();
            teamResponse.Id = team.Id;
            teamResponse.Name = team.Name;
            teamResponse.CreatedAt = team.CreatedAt;
            teamResponse.UpdatedAt = team.UpdatedAt;

            teamResponse.TeamMembers = new List<TeamMemberResponse>();
            foreach (var teamMember in team.TeamMembers)
            {
                TeamMemberResponse teamMemberResponse = new TeamMemberResponse();
                teamMemberResponse.TeamId = teamMember.TeamId;
                teamMemberResponse.UserId = teamMember.UserId;

                UserResponse userResponse = new UserResponse();
                userResponse.Id = teamMember.User.Id;
                userResponse.FName = teamMember.User.FName;
                userResponse.LName = teamMember.User.LName;
                userResponse.ContactNo = teamMember.User.ContactNo;
                userResponse.Email = teamMember.User.Email;

                teamMemberResponse.User = userResponse;
                teamResponse.TeamMembers.Add(teamMemberResponse);
            }

            return teamResponse;
        }

        // Create New Team
        public TeamDto CreateTeam(TeamDto team)
        {
            Team newTeam = _mapper.Map<Team>(team);
            newTeam.CreatedAt = DateTime.Now;
            newTeam.UpdatedAt = DateTime.Now;

            try
            {
                _context.Add(newTeam);
                int isSaved = _context.SaveChanges();
                return isSaved > 0 ? _mapper.Map<TeamDto>(newTeam) : null;
            }
            catch
            {
                return null;
            }
        }

        // Update Team
        public bool UpdateTeam(long id, TeamDto team)
        {
            try
            {
                team.UpdatedAt = DateTime.Now;
                Team updateTeam = _mapper.Map<Team>(team);
                _context.Entry(updateTeam).State = EntityState.Modified;
                _context.Teams.Update(updateTeam);
                int isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        // Delete Team
        public bool DeleteTeam(long id)
        {
            Team? team = _context.Teams.Find(id);
            if (team == null)
            {
                return false;
            }

            try
            {
                _context.Teams.Remove(team);
                int isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        // Check Team Exists
        public bool IsTeamExists(long id)
        {
            return _context.Teams.Any(t => t.Id == id);
        }

        // Check Team Exists
        public bool IsTeamExists(string name)
        {
            return _context.Teams.Any(t => t.Name == name);
        }

        // Get Total Teams
        public int GetTotalTeams()
        {
            return _context.Teams.Count();
        }
    }
}
