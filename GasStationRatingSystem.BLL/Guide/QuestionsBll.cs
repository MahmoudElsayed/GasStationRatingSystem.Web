using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
    public class QuestionsBll
    {
        #region Fields

        private readonly IRepository<Question> _repoQuestion;
        private readonly IRepository<Answer> _repoAnswer;
        private readonly IRepository<AnswerCategory> _repoAnswerCategory;


        public QuestionsBll(IRepository<Question> repoQuestion, IRepository<Answer> repoAnswer, IRepository<AnswerCategory> repoAnswerCategory)
        {
            _repoQuestion = repoQuestion;
            _repoAnswer = repoAnswer;
            _repoAnswerCategory = repoAnswerCategory;
        }

        #endregion
        #region Get
        public ResultDTO GetQuestions()
        {
            ResultDTO result = new ResultDTO();
            var lst = new List<object>();
             _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).ToList().ForEach(
                p =>
                {
                    var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false&&p.QuestionParentNo==1).FirstOrDefault();
                    var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && p.QuestionParentNo == 2).FirstOrDefault();

                    lst.Add(
                        new
                        {
                           // ParentItemId = p.ID,
                            ParentItemText = p.Text,
                           // ItemOneId = Item?.ID??null,
                            ItemOneText = Item?.Text ?? "",
                           // ItemTwoId = Item2?.ID ?? null,
                            ItemTwoText = Item2?.Text ?? "",

                            Questions = _repoQuestion.GetAllAsNoTracking().Where(qu =>qu.ParentId == (Item!=null?Item.ID: p.ID) && qu.IsLast == true).ToList().Select(
                                q=>new {Id=q.ID, QuestionText=q.Text
                                , Answers=_repoAnswer.GetAllAsNoTracking().Where(an=>an.QuestionId==q.ID).ToList().GroupBy(p=>p.AnswerCategoryId).Select(AnswerCategory =>
                                new {
                                AnswerCategoryId= AnswerCategory.Key, Label= _repoAnswerCategory.GetAllAsNoTracking().Where(cat=>cat.ID==AnswerCategory.Key).FirstOrDefault().Text
                                , Options= _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID&& Options.AnswerCategoryId==AnswerCategory.Key).ToList().OrderBy(p=>p.OrderNo).Select(
                                    
                                    op => new {OptionId=op.ID, OptionText=op.Text}
                                    )
                                })
                                }
                                )

                        });
                }
                ) ;
            result.data = new { Items = lst };
            return result;
        }
        public Question GetById(Guid id)
        {

            return _repoQuestion.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
        }
        #endregion
    }
}
