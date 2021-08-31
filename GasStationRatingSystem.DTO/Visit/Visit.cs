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
    public class SaveVisitAnswersDTO
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.VisitIdRequired))]

        public Guid? VisitId { get; set; }
        public int PageIndex { get; set; }
        public List<OptionsDTO> Options { get; set; }
    }
    public class SaveOfflineVisitAnswersDTO
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.VisitIdRequired))]

        public List<SaveVisitAnswersDTO> SaveVisitsAnswers { get; set; }
    }
    public class OptionsDTO
    {
        public Guid? OptionId { get; set; }
        public Guid? RefId { get; set; }
    }
}
