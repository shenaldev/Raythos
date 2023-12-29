using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/team/member")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberInterface _teamMemberRepository;
        private readonly ITeamInterface _teamRepository;

        public TeamMembersController(
            ITeamMemberInterface teamMemberRepository,
            ITeamInterface teamRepository
        )
        {
            _teamMemberRepository = teamMemberRepository;
            _teamRepository = teamRepository;
        }

        public ITeamInterface TeamRepository { get; }

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

            if (teamMember.UserId == 0)
            {
                ErrorResponse error = new ErrorResponse(401, "UserId Is Required Field");
                return StatusCode(401, error);
            }

            if (_teamMemberRepository.IsTeamMemberExists(teamId, teamMember.UserId))
            {
                ErrorResponse error = new ErrorResponse(401, "User already exists in this team");
                return StatusCode(401, error);
            }

            TeamMemberDto newTeamMember = _teamMemberRepository.CreateTeamMember(
                teamId,
                teamMember
            );

            if (newTeamMember == null)
            {
                ErrorResponse error = new ErrorResponse(500, "Something Went Wrong");
                return StatusCode(500, error);
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

            if (!IsDeleted)
            {
                ErrorResponse error = new ErrorResponse(500, "Something Went Wrong");
                return StatusCode(500, error);
            }

            return Ok("Member deleted successfully.");
        }
    }
}
