using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/team")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamInterface _teamRepository;

        public TeamsController(ITeamInterface teamRepository)
        {
            _teamRepository = teamRepository;
        }

        // GET: api/dashboard/team
        [HttpGet]
        public ActionResult<PaginatedResponse<TeamDto>> GetTeams([FromQuery] int page = 1)
        {
            var take = 15;
            var skip = (page - 1) * take;
            int totalTeams = _teamRepository.GetTotalTeams();
            int lastPage = (int)Math.Ceiling((double)totalTeams / take);

            ICollection<TeamDto> teams = _teamRepository.GetTeams(skip, take);
            if (teams == null)
            {
                return NotFound();
            }

            return PaginatedResponse<TeamDto>.Paginate(teams, totalTeams, page, lastPage);
        }

        // GET: api/dashboard/team/5
        [HttpGet("{id}")]
        public IActionResult GetTeam(long id)
        {
            if (!_teamRepository.IsTeamExists(id))
            {
                return NotFound();
            }

            TeamResponse team = _teamRepository.GetTeam(id);
            return Ok(team);
        }

        // POST: api/dashboard/team
        [HttpPost]
        public IActionResult CreateTeam([FromForm] TeamDto team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check Team Name Exists
            bool IsTeamExist = _teamRepository.IsTeamExists(team.Name);
            if (IsTeamExist)
            {
                ErrorResponse error = new ErrorResponse(400, "Team Name Already Exists");
                return BadRequest(error);
            }

            TeamDto newTeam = _teamRepository.CreateTeam(team);
            if (newTeam == null)
            {
                ErrorResponse error = new ErrorResponse(500, "Something Went Wrong");
                return StatusCode(500, error);
            }

            return Ok(newTeam);
        }

        // PUT: api/dashboard/team/5
        [HttpPut("{id}")]
        public IActionResult UpdateTeam(long id, [FromForm] TeamDto team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (team.Id != id)
            {
                ErrorResponse error = new ErrorResponse(401, "Invalid Team Id");
                return BadRequest(error);
            }

            if (!_teamRepository.IsTeamExists(id))
            {
                return NotFound();
            }

            //Check Team Name Exists
            bool IsTeamExist = _teamRepository.IsTeamExists(team.Name);
            if (IsTeamExist)
            {
                ErrorResponse error = new ErrorResponse(400, "Team Name Already Exists");
                return BadRequest(error);
            }

            bool isUpdated = _teamRepository.UpdateTeam(id, team);
            if (!isUpdated)
            {
                ErrorResponse error = new ErrorResponse(500, "Something Went Wrong");
                return StatusCode(500, error);
            }

            return Ok();
        }

        // DELETE: api/dashboard/team/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(long id)
        {
            if (!_teamRepository.IsTeamExists(id))
            {
                return NotFound();
            }

            bool isDeleted = _teamRepository.DeleteTeam(id);
            if (!isDeleted)
            {
                ErrorResponse error = new ErrorResponse(500, "Something Went Wrong");
                return StatusCode(500, error);
            }

            return Ok();
        }
    }
}
