using AutoMapper;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class TeamMemberRepository : ITeamMemberInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamMemberRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<TeamMemberDto> GetTeamMembers(long teamId)
        {
            throw new NotImplementedException();
        }

        public TeamMemberDto CreateTeamMember(long teamId, TeamMemberDto teamMember)
        {
            TeamMember newMember = _mapper.Map<TeamMember>(teamMember);
            newMember.CreatedAt = DateTime.Now;
            newMember.UpdatedAt = DateTime.Now;

            try
            {
                _context.Add(newMember);
                int isSaved = _context.SaveChanges();
                return isSaved > 0 ? _mapper.Map<TeamMemberDto>(newMember) : null;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteTeamMember(long teamId, long userID)
        {
            TeamMember? teamMember = _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Where(tm => tm.UserId == userID)
                .FirstOrDefault();
            if (teamMember == null)
            {
                return false;
            }

            try
            {
                _context.TeamMembers.Remove(teamMember);
                int isSaved = _context.SaveChanges();
                return isSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool IsTeamMemberExists(long teamId, long userID)
        {
            return _context.TeamMembers.Any(tm => tm.TeamId == teamId && tm.UserId == userID);
        }
    }
}
