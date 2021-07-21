using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(GasStation) + "s", Schema = AppConstants.Areas.Guide)]

    public class GasStation:BaseEntity
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string LauncherName { get; set; }
        public ICollection<GasStationContact> GasStationContacts { get; set; }
    }
    [Table(nameof(GasStationContact) + "s", Schema = AppConstants.Areas.Guide)]

    public class GasStationContact:BaseEntity
    {
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
        [ForeignKey(nameof(GasStation))]
        public Guid GasStationId { get; set; }
        public virtual GasStation GasStation { get; set; }
    }
    public enum ContactType
    {
        Phone,Email
    }
}
