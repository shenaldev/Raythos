﻿using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Aircrafts;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Public
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController : ControllerBase
    {
        private readonly IAircraftInterface _aircraftRepository;

        public AircraftsController(IAircraftInterface aircraftInterface)
        {
            _aircraftRepository = aircraftInterface;
        }

        // GET: api/aircrafts
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
    }
}
