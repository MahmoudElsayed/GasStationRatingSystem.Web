using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(VisitInfo) , Schema = AppConstants.Areas.Visit)]

    public class VisitInfo:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        [ForeignKey(nameof(GasStation))]

        public Guid StationId { get; set; }
        public DateTime VisitTime { get; set; } = AppDateTime.Now;
        public string VisitNo { get; set; } = AppDateTime.Now.ToString("yyyyMMddHHmm",new CultureInfo("en-US"))+new Random((int)AppDateTime.Now.Ticks).Next(1111, 9999);
        public virtual  User User { get; set; }
        public virtual GasStation GasStation { get; set; }

    }
}
