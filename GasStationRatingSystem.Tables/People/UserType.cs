using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.Tables
{
   public class UserType:BaseEntity
    {
        public string TypeName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
