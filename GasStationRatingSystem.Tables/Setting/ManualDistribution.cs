using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(ManualDistribution), Schema = AppConstants.Areas.Setting)]

    public class ManualDistribution:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        [ForeignKey(nameof(GasStation))]

        public Guid? StationId { get; set; }
        public virtual GasStation GasStation { get; set; }
        public virtual User User { get; set; }

    }
}
