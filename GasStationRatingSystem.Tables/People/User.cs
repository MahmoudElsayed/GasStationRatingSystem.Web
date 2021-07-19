using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(User) + "s", Schema = AppConstants.Areas.People)]

    public class User:BaseEntity
    {
        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;
        /// <summary>
        /// هل قام باستخدام الباسورد الافتراضي ام لا
        /// </summary>
        public bool UseDefaultPassword { get; set; } = true;

        #region Reset

        public DateTime? ResetPasswordDate { get; set; }

        public string CodeOfReset { get; set; } = string.Empty;

        #endregion

        /// <summary>
        /// For Password 
        /// </summary>
        public string Salt { get; set; }
        [ForeignKey(nameof(UserType))]
        public Guid UserTypeId { get; set; }
        #region Relations

        public virtual UserType UserType { get; set; }
        #region User
        public ICollection<User> UserCreated { get; set; }
        public ICollection<User> UserModified { get; set; }
        public ICollection<User> UserDeleted { get; set; }
        public ICollection<UserType> UserTypeCreated { get; set; }
        public ICollection<UserType> UserTypeModified { get; set; }
        public ICollection<UserType> UserTypeDeleted { get; set; }




        #endregion

        #endregion
    }
}
