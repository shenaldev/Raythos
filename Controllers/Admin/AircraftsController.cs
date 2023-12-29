using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;

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
            var newAircraft = _aircraftRepository.CreateAircraft(aircraft);

            // TODO: IMPLEMENT FILE UPLOAD

            if (newAircraft == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(newAircraft);
        }
    }
}
