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
        #region Guide
        public ICollection<GasStation> GasStationCreated { get; set; }
        public ICollection<GasStation> GasStationModified { get; set; }
        public ICollection<GasStation> GasStationDeleted { get; set; }
        public ICollection<GasStationContact> GasStationContactCreated { get; set; }
        public ICollection<GasStationContact> GasStationContactModified { get; set; }
        public ICollection<GasStationContact> GasStationContactDeleted { get; set; }
        #region Questions
        public ICollection<Question> QuestionCreated { get; set; }
        public ICollection<Question> QuestionModified { get; set; }
        public ICollection<Question> QuestionDeleted { get; set; }

        public ICollection<AnswerCategory> AnswerCategoryCreated { get; set; }
        public ICollection<AnswerCategory> AnswerCategoryModified { get; set; }
        public ICollection<AnswerCategory> AnswerCategoryDeleted { get; set; }

        public ICollection<Answer> AnswerCreated { get; set; }
        public ICollection<Answer> AnswerModified { get; set; }
        public ICollection<Answer> AnswerDeleted { get; set; }
        #endregion
        #endregion
        #region Visit
        public ICollection<VisitInfo> VisitInfoCreated { get; set; }
        public ICollection<VisitInfo> VisitInfoModified { get; set; }
        public ICollection<VisitInfo> VisitInfoDeleted { get; set; }
        #endregion
        
        #endregion
    }
}
