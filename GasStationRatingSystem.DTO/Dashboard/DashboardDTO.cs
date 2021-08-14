using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
  public  class DashboardGetTotalsDTO
    {
        public string CityName { get; set; }
        public int Total { get; set; }
        public int InProgress { get; set; }
        public int Ended { get; set; }
        public int NotStarted => Total-(InProgress + Ended);

    }
}
