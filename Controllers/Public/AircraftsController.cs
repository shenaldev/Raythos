using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Aircrafts;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Public
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController : ControllerBase
    {
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftsController(IAircraftRepository aircraftInterface)
        {
            _aircraftRepository = aircraftInterface;
        }

        // GET: api/aircrafts
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<AircraftDto>>> GetAircraftsAsync(
            [FromQuery] int page = 1
        )
        {
            int take = 15;
            var skip = (page - 1) * take;
            int totalTeams = await _aircraftRepository.GetTotalAircrafts();
            int lastPage = (int)Math.Ceiling((double)totalTeams / take);

            ICollection<AircraftDto> aircrafts = await _aircraftRepository.GetAircrafts(skip, take);
            if (aircrafts == null)
            {
                return NotFound();
            }

            return PaginatedResponse<AircraftDto>.Paginate(aircrafts, totalTeams, page, lastPage);
        }

        // GET: api/dashboard/admin/aircraft/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftSingleDto>> GetAircraftAsync(long id)
        {
            if (!await _aircraftRepository.IsAircraftExists(id))
            {
                return NotFound();
            }

            AircraftSingleDto? aircraft = await _aircraftRepository.GetAircraft(id);
            return Ok(aircraft);
        }
    }
}
