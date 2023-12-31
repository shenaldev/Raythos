using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Responses;
using System.Globalization;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/aircraft")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AircraftsController : ControllerBase
    {
        private readonly IAircraftInterface _aircraftRepository;

        public AircraftsController(IAircraftInterface aircraftRepository)
        {
            _aircraftRepository = aircraftRepository;
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
        public ActionResult<AircraftDto> GetAircraft(long id)
        {
            if (!_aircraftRepository.IsAircraftExists(id))
            {
                return NotFound();
            }

            AircraftDto aircraft = _aircraftRepository.GetAircraft(id);
            return Ok(aircraft);
        }

        // POST: api/dashboard/admin/aircraft
        [HttpPost]
        public ActionResult PostAircraft([FromForm] AircraftDto aircraft)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string slug = aircraft.Model.ToLower().Replace(" ", "-") + aircraft.SerialNumber;
            aircraft.Slug = slug;
            AircraftDto newAircraft = _aircraftRepository.CreateAircraft(aircraft);

            // TODO: IMPLEMENT FILE UPLOAD

            if (newAircraft == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
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
