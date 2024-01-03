using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.DTOs.Aircrafts;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/aircraft")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AircraftsController : ControllerBase
    {
        private readonly IAircraftInterface _aircraftRepository;
        private readonly IAircraftOptionInterface _aircraftOptionsRepository;
        private readonly ITeamInterface _teamRepository;

        public AircraftsController(
            IAircraftInterface aircraftRepository,
            IAircraftOptionInterface aircraftOptionsRepository,
            ITeamInterface teamRepository
        )
        {
            _aircraftRepository = aircraftRepository;
            _aircraftOptionsRepository = aircraftOptionsRepository;
            _teamRepository = teamRepository;
        }

        // GET: api/dashboard/admin/aircraft
        [HttpGet]
        public ActionResult<PaginatedResponse<AircraftDto>> GetAircrafts([FromQuery] int page = 1)
        {
            int take = 15;
            var skip = (page - 1) * take;
            int totalTeams = _aircraftRepository.GetTotalAircrafts();
            int lastPage = (int)Math.Ceiling((double)totalTeams / take);

            ICollection<AircraftDto> aircrafts = _aircraftRepository.GetAircrafts(skip, take);
            if (aircrafts == null)
            {
                return NotFound();
            }

            return PaginatedResponse<AircraftDto>.Paginate(aircrafts, totalTeams, page, lastPage);
        }

        // GET: api/dashboard/admin/aircraft/5
        [HttpGet("{id}")]
        public ActionResult<AircraftSingleDto> GetAircraft(long id)
        {
            if (!_aircraftRepository.IsAircraftExists(id))
            {
                return NotFound();
            }

            AircraftSingleDto aircraft = _aircraftRepository.GetAircraft(id);
            return Ok(aircraft);
        }

        // POST: api/dashboard/admin/aircraft
        [HttpPost]
        public ActionResult PostAircraft(
            [FromForm] AircraftPostDto aircraft,
            [FromForm] List<AircraftOptionDto> aircraftOptions
        )
        {
            // Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (aircraft.TeamId == null)
                return BadRequest(new { message = "Team ID is required" });

            if (_aircraftRepository.IsTeamAssigned((long)aircraft.TeamId))
                return BadRequest(new { message = "Team is already assigned to another aircraft" });

            if (!_teamRepository.IsTeamExists((long)aircraft.TeamId))
                return BadRequest(new { message = "Team does not exist" });

            // Create Slug
            string slug = aircraft.Model.ToLower().Replace(" ", "-") + aircraft.SerialNumber;
            aircraft.Slug = slug;
            AircraftPostDto newAircraft = _aircraftRepository.CreateAircraft(aircraft);

            // TODO: IMPLEMENT FILE UPLOAD

            if (newAircraft == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                foreach (AircraftOptionDto option in aircraftOptions)
                {
                    option.AircraftId = newAircraft.Id;
                    _aircraftOptionsRepository.AddCustomization(option);
                }
            }

            return Ok(newAircraft);
        }

        // PUT: api/dashboard/admin/aircraft/5
        [HttpPut("{id}")]
        public ActionResult PutAircraft(long id, [FromForm] AircraftDto aircraft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_aircraftRepository.IsAircraftExists(id))
            {
                return NotFound();
            }

            bool isUpdated = _aircraftRepository.UpdateAircraft(id, aircraft);
            if (isUpdated)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/dashboard/admin/aircraft/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAircraft(long id)
        {
            if (!_aircraftRepository.IsAircraftExists(id))
            {
                return NotFound();
            }

            bool isDeleted = _aircraftRepository.DeleteAircraft(id);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
