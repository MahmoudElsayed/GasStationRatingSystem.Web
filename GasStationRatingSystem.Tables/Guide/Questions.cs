using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    #region Questions

    [Table(nameof(Question) + "s", Schema = AppConstants.Areas.Guide)]

    public class Question:BaseEntity
    {
        [ForeignKey(nameof(tblQuestion))]
        public Guid? ParentId { get; set; }
        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }
        public int? QuestionParentNo { get; set; }

        public int? OrderNo { get; set; }
        public bool? IsLast { get; set; } = false;
        public virtual Question tblQuestion { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
    public enum QuestionType {Normal, DropdownLicensedInstitution }
    #endregion

    [Table("AnswerCategories", Schema = AppConstants.Areas.Guide)]

    public class AnswerCategory : BaseEntity
    {
        public string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
    [Table(nameof(Answer)+"s", Schema = AppConstants.Areas.Guide)]

    public class Answer : BaseEntity
    {
        [ForeignKey(nameof(Question))]
        public Guid? QuestionId { get; set; }
        [ForeignKey(nameof(AnswerCategory))]
        public Guid? AnswerCategoryId { get; set; }
        public string Text { get; set; }
        public int? OrderNo { get; set; }
        public int? AnswerCategoryOrderNo { get; set; }

        public virtual Question Question { get; set; }
        public virtual AnswerCategory AnswerCategory { get; set; }
    }
}
