using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/team")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
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
            var errors = new Dictionary<string, List<string>>();

            //Check Team Name Exists
            bool IsTeamExist = _teamRepository.IsTeamExists(team.Name);
            if (IsTeamExist)
            {
                errors["Name"] = new List<string> { "Team is already exists" };
                var error = ErrorResponse.CreateValidationError(errors);
                return BadRequest(error);
            }

            TeamDto newTeam = _teamRepository.CreateTeam(team);
            if (newTeam == null)
            {
                errors["Name"] = new List<string> { "Something went wrong." };
                var error = ErrorResponse.CreateValidationError(errors);
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

            var errors = new Dictionary<string, List<string>>();

            if (team.Id != id)
            {
                errors["Name"] = new List<string> { "Invalid Team Id" };
                var error = ErrorResponse.CreateValidationError(errors);
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
                errors["Name"] = new List<string> { "Team is already exists" };
                var error = ErrorResponse.CreateValidationError(errors);
                return BadRequest(error);
            }

            bool isUpdated = _teamRepository.UpdateTeam(id, team);
            if (!isUpdated)
            {
                errors["Name"] = new List<string> { "Something went wrong." };
                var error = ErrorResponse.CreateValidationError(errors);
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

            var errors = new Dictionary<string, List<string>>();

            bool isDeleted = _teamRepository.DeleteTeam(id);
            if (!isDeleted)
            {
                errors["Name"] = new List<string> { "Something went wrong." };
                var error = ErrorResponse.CreateValidationError(errors);
                return StatusCode(500, error);
            }

            return Ok();
        }
    }
}
