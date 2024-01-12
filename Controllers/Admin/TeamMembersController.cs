using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/team/member")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberInterface _teamMemberRepository;
        private readonly ITeamRepository _teamRepository;

        public TeamMembersController(
            ITeamMemberInterface teamMemberRepository,
            ITeamRepository teamRepository
        )
        {
            _teamMemberRepository = teamMemberRepository;
            _teamRepository = teamRepository;
        }

        public ITeamRepository TeamRepository { get; }

        // GET: api/dashboard/admin/team/member/5
        [HttpGet("{teamId}")]
        public ActionResult<ICollection<TeamMemberDto>> GetTeamMembers(long teamId)
        {
            if (!_teamRepository.IsTeamExists(teamId))
            {
                return NotFound();
            }

            return Ok(_teamMemberRepository.GetTeamMembers(teamId));
        }

        // POST: api/dashboard/admin/team/member/5
        [HttpPost("{teamId}")]
        public ActionResult<TeamMemberDto> CreateTeamMember(
            long teamId,
            [FromForm] TeamMemberDto teamMember
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_teamRepository.IsTeamExists(teamId))
            {
                return NotFound();
            }

            var errors = new Dictionary<string, List<string>>();

            if (teamMember.UserId == 0)
            {
                errors["Name"] = new List<string> { "UserId Is Required Field." };
                return StatusCode(401, errors);
            }

            if (_teamMemberRepository.IsTeamMemberExists(teamId, teamMember.UserId))
            {
                errors["Name"] = new List<string> { "User already exists in this team." };
                return StatusCode(401, errors);
            }

            TeamMemberDto newTeamMember = _teamMemberRepository.CreateTeamMember(
                teamId,
                teamMember
            );

            if (newTeamMember == null)
            {
                errors["Name"] = new List<string> { "Something went wrong." };
                return StatusCode(500, errors);
            }

            return Ok(newTeamMember);
        }

        // DELETE: api/dashboard/admin/team/member/1/5 {teamId}/{userId}
        [HttpDelete("{teamId}/{userId}")]
        public ActionResult<TeamMemberDto> DeleteTeamMember(long teamId, long userId)
        {
            if (!_teamRepository.IsTeamExists(teamId))
            {
                return NotFound();
            }

            if (!_teamMemberRepository.IsTeamMemberExists(teamId, userId))
            {
                return NotFound();
            }

            bool IsDeleted = _teamMemberRepository.DeleteTeamMember(teamId, userId);
            var errors = new Dictionary<string, List<string>>();

            if (!IsDeleted)
            {
                errors["Name"] = new List<string> { "Something went wrong." };
                return StatusCode(500, errors);
            }

            return Ok("Member deleted successfully.");
        }
    }
}
