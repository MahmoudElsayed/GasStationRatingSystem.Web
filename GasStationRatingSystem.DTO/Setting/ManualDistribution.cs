using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
   public class ManualDistributionDTO
    {
        public int TotalCount { get; set; }
        public string AddedDate { get; set; }
        public Guid ID { get; set; }
        public Guid? UserId { get; set; }
        public Guid? StationId { get; set; }
        public Guid? CityId { get; set; }

        public Guid? DistrictId { get; set; }
        public string UserName { get; set; }
        public string StationName { get; set; }

    }
}
