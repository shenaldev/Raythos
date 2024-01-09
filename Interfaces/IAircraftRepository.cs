﻿using Raythos.DTOs.Aircrafts;

namespace Raythos.Interfaces
{
    public interface IAircraftRepository
    {
        ICollection<AircraftDto> GetAircrafts(int skip, int take = 15);

        AircraftSingleDto GetAircraft(long id);

        AircraftPostDto CreateAircraft(AircraftPostDto aircraft);

        bool UpdateAircraft(long id, AircraftDto aircraft);

        bool DeleteAircraft(long id);

        bool IsAircraftExists(long id);

        int GetTotalAircrafts();

        bool IsTeamAssigned(long teamId);
    }
}
