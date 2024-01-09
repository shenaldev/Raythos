using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/aircraft/customization")]
    [ApiController]
    public class AircraftOptionController : ControllerBase
    {
        private readonly IAircraftOptionRepository _aircraftOptionsRepository;
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftOptionController(
            IAircraftOptionRepository aircraftOptionsRepository,
            IAircraftRepository aircraftRepository
        )
        {
            _aircraftOptionsRepository = aircraftOptionsRepository;
            _aircraftRepository = aircraftRepository;
        }

        // GET: api/dashboard/admin/aircraft/customization
        [HttpGet]
        public async Task<
            ActionResult<PaginatedResponse<AircraftOptionDto>>
        > GetAllAircraftCustAsync([FromQuery] int page = 1)
        {
            int take = 15;
            var skip = (page - 1) * take;
            int total = await _aircraftOptionsRepository.GetTotalCustomizations();
            int lastPage = (int)Math.Ceiling((double)total / take);

            ICollection<AircraftOptionDto> customizations =
                await _aircraftOptionsRepository.GetAircraftCustomizations(skip, take);
            if (customizations == null)
            {
                return NotFound();
            }

            return PaginatedResponse<AircraftOptionDto>.Paginate(
                customizations,
                total,
                page,
                lastPage
            );
        }

        // GET: api/dashboard/admin/aircraft/customization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AircraftOptionDto>> GetAircraftCustAsync(long id)
        {
            if (!await _aircraftOptionsRepository.IsCustomizationExists(id))
            {
                return NotFound();
            }

            AircraftOptionDto? customization =
                await _aircraftOptionsRepository.GetAircraftCustomization(id);
            return Ok(customization);
        }

        // GET: api/dashboard/admin/aircraft/customization/aircraft/5
        [HttpGet("aircraft/{aircraftId}")]
        public async Task<
            ActionResult<ICollection<AircraftOptionDto>>
        > GetAircraftCustByAircraftIdAsync(long aircraftId)
        {
            if (!await _aircraftRepository.IsAircraftExists(aircraftId))
            {
                return NotFound();
            }

            ICollection<AircraftOptionDto> customizations =
                await _aircraftOptionsRepository.GetAircraftCustomizationByAircraftId(aircraftId);
            return Ok(customizations);
        }
    }
}
