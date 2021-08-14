using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table("Cities", Schema = AppConstants.Areas.Guide)]

    public class City:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
    [Table( nameof(District)+"s", Schema = AppConstants.Areas.Guide)]

    public class District : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey(nameof(City))]
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }
        public ICollection<GasStation> GasStations { get; set; }

    }
}
