using GasStationRatingSystem.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class CreateVisitDTO
    {
      //  [Required(ErrorMessage = nameof(GasStationRatingSystemResources.EnvironmentalSpecialistRequired))]

        public Guid? UserId { get; set; } = null;
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.StationRequired))]

        public Guid? StationId { get; set; } = null;

    }
}
