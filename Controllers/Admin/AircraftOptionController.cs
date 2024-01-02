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
        private readonly IAircraftOptionInterface _aircraftOptionsRepository;
        private readonly IAircraftInterface _aircraftRepository;

        public AircraftOptionController(
            IAircraftOptionInterface aircraftOptionsRepository,
            IAircraftInterface aircraftRepository
        )
        {
            _aircraftOptionsRepository = aircraftOptionsRepository;
            _aircraftRepository = aircraftRepository;
        }

        // GET: api/dashboard/admin/aircraft/customization
        [HttpGet]
        public ActionResult<PaginatedResponse<AircraftOptionDto>> GetAllAircraftCust(
            [FromQuery] int page = 1
        )
        {
            int take = 15;
            var skip = (page - 1) * take;
            int total = _aircraftOptionsRepository.GetTotalCustomizations();
            int lastPage = (int)Math.Ceiling((double)total / take);

            ICollection<AircraftOptionDto> customizations =
                _aircraftOptionsRepository.GetAircraftCustomizations(skip, take);
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
        public ActionResult<AircraftOptionDto> GetAircraftCust(long id)
        {
            if (!_aircraftOptionsRepository.IsCustomizationExists(id))
            {
                return NotFound();
            }

            AircraftOptionDto customization = _aircraftOptionsRepository.GetAircraftCustomization(
                id
            );
            return Ok(customization);
        }

        // GET: api/dashboard/admin/aircraft/customization/aircraft/5
        [HttpGet("aircraft/{aircraftId}")]
        public ActionResult<ICollection<AircraftOptionDto>> GetAircraftCustByAircraftId(
            long aircraftId
        )
        {
            if (!_aircraftRepository.IsAircraftExists(aircraftId))
            {
                return NotFound();
            }

            ICollection<AircraftOptionDto> customizations =
                _aircraftOptionsRepository.GetAircraftCustomizationByAircraftId(aircraftId);
            return Ok(customizations);
        }
    }
}
