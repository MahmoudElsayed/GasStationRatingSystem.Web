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

        public string GetTextOneOfQuestion(Guid questionId,int questionParentNo)
        {
           var QestionText =  _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == questionId && a.IsLast == false && a.QuestionParentNo == questionParentNo).FirstOrDefault()?.Text??"";
           return QestionText;
        }

        public ResultDTO GetQuestions()
        {
            ResultDTO result = new ResultDTO();
            var lst = new List<object>();
            int PageIndex = 1;
             _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).OrderBy(p => p.OrderNo).ToList().ForEach(
                p =>
                {

                    var Items = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false&&a.QuestionParentNo.Value==1);
                    if (Items.Count()!=0)
                    {
                        foreach (var Item in Items)
                        {
                            if (Item.ParentId.Value==Guid.Parse("bad98ed3-3db4-4b5c-a21f-0a96c367f06e"))
                            {

                            }
                            
                            {
                                var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == Item.ID && a.IsLast == false && a.QuestionParentNo.Value == 2).FirstOrDefault();
                                string Item2Text = Item2?.Text ?? "";
                                if (Item2 == null)
                                {
                                    Item2 = Item;
                                   
                                }
                                lst.Add(
                        new
                        {
                            PageIndex=PageIndex++,
                        // ParentItemId = p.ID,
                        ParentItemText = p.Text,
                        // ItemOneId = Item?.ID??null,
                        // ItemTwoId = Item2?.ID ?? null,
                        ItemOneText = Item.Text,
                            Items = new
                            {
                                ItemTwoText = Item2Text,
                                Questions =
                            _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true).ToList().Select(
                                q => new
                                {
                                    Id = q.ID,
                                    QuestionText = q.Text,
                                    QuestionTypeNo=(int)q.QuestionType,
                                    MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                                    ListOfReference=q.QuestionType== QuestionType.DropdownLicensedInstitution? new List<object>() { new {Id=Guid.NewGuid(),Text="جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } }:null,
                                    Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID).OrderBy(p=>p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                      new
                                      {
                                          AnswerCategoryId = Guid.NewGuid(),
                                          Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                      ,
                                          Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                          op => new { OptionId = op.ID, OptionText = op.Text }
                                          )
                                      })
                                }
                                )
                            }



                        });
                            }


                        }
                    }
                    else
                    {
                        var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 1).FirstOrDefault();
                        var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 2).FirstOrDefault();


                        if (Item2 == null)
                        {
                            Item2 = Item;
                        }
                        lst.Add(
                            new
                            {
                                PageIndex = PageIndex++,

                                // ParentItemId = p.ID,
                                ParentItemText = p.Text,
                                // ItemOneId = Item?.ID??null,
                                // ItemTwoId = Item2?.ID ?? null,
                                ItemOneText = GetTextOneOfQuestion(p.ID, 1),
                                Items = new
                                {
                                    ItemTwoText = GetTextOneOfQuestion(p.ID, 2),
                                    Questions =
                                _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true).ToList().Select(
                                    q => new
                                    {
                                        Id = q.ID,
                                        QuestionText = q.Text,
                                        QuestionTypeNo = (int)q.QuestionType,

                                        MultipleAnswerCategory = q.HasMultiCategoryAnswer,

                                        Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                          new
                                          {
                                              AnswerCategoryId = Guid.NewGuid(),
                                              Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                          ,
                                              Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                              op => new { OptionId = op.ID, OptionText = op.Text }
                                              )
                                          })
                                    }
                                    )
                                }



                            });
                    }
                }
                ) ;
            result.data = new { Items = lst };
            return result;
        }
        public ResultDTO GetPendingQuestions(Guid visitId)
        {
            ResultDTO result = new ResultDTO();
            var _repoVisitInfo=_repoQuestion.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<VisitInfo>) ) as IRepository<VisitInfo>;
            var visit = _repoVisitInfo.GetAllAsNoTracking().Where(p => p.ID == visitId).FirstOrDefault();
            int pageIndex = 0;
            if (visit != null)
            {
                pageIndex = visit.PageIndex.Value;
            }
            int PageNo = 1;
            var lst = new List<object>();
            _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).OrderBy(p => p.OrderNo).ToList().ForEach(
               p =>
               {

                   var Items = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo.Value == 1);
                   if (Items.Count() != 0)
                   {
                       foreach (var Item in Items)
                       {
                           if (Item.ParentId.Value == Guid.Parse("bad98ed3-3db4-4b5c-a21f-0a96c367f06e"))
                           {

                           }

                           {
                               var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == Item.ID && a.IsLast == false && a.QuestionParentNo.Value == 2).FirstOrDefault();
                               string Item2Text = Item2?.Text ?? "";
                               if (Item2 == null)
                               {
                                   Item2 = Item;

                               }
                               lst.Add(
                       new
                       {
                           PageIndex = PageNo++,

                           // ParentItemId = p.ID,
                           ParentItemText = p.Text,
                            // ItemOneId = Item?.ID??null,
                            // ItemTwoId = Item2?.ID ?? null,
                            ItemOneText = Item.Text,
                           Items = new
                           {
                               ItemTwoText = Item2Text,
                               Questions =
                           _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true).ToList().Select(
                               q => new
                               {
                                   Id = q.ID,
                                   QuestionText = q.Text,
                                   QuestionTypeNo = (int)q.QuestionType,
                                   MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                                   ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                                   Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                     new
                                     {
                                         AnswerCategoryId = Guid.NewGuid(),
                                         Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                     ,
                                         Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                         op => new { OptionId = op.ID, OptionText = op.Text }
                                         )
                                     })
                               }
                               )
                           }



                       });
                           }


                       }
                   }
                   else
                   {
                       var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 1).FirstOrDefault();
                       var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 2).FirstOrDefault();


                       if (Item2 == null)
                       {
                           Item2 = Item;
                       }
                       lst.Add(
                           new
                           {
                               PageIndex = PageNo++,

                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                                // ItemOneId = Item?.ID??null,
                                // ItemTwoId = Item2?.ID ?? null,
                                ItemOneText = GetTextOneOfQuestion(p.ID, 1),
                               Items = new
                               {
                                   ItemTwoText = GetTextOneOfQuestion(p.ID, 2),
                                   Questions =
                               _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true).ToList().Select(
                                   q => new
                                   {
                                       Id = q.ID,
                                       QuestionText = q.Text,
                                       QuestionTypeNo = (int)q.QuestionType,

                                       MultipleAnswerCategory = q.HasMultiCategoryAnswer,

                                       Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                         new
                                         {
                                             AnswerCategoryId = Guid.NewGuid(),
                                             Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                         ,
                                             Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                             op => new { OptionId = op.ID, OptionText = op.Text }
                                             )
                                         })
                                   }
                                   )
                               }



                           });
                   }
               }
               );
           
            result.data = new { Items = lst.Skip(pageIndex) };
            return result;
        }
        public ResultDTO GetVisits()
        {
            ResultDTO result = new ResultDTO();
            var _repoVisitInfo = _repoQuestion.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<VisitInfo>)) as IRepository<VisitInfo>;
            result.data = _repoVisitInfo.GetAllAsNoTracking().Include(p=>p.GasStation).Where(p => p.IsActive && !p.IsDeleted && p.PageIndex < 13).Select(p=>new { 
             VisitId=p.ID,
             PageIndex=p.PageIndex,
             VisitNo=p.VisitNo,
             VisitTime=p.VisitTime.ToString("dd/MM/yyyy hh:mm:ss tt"),
             StationName=p.GasStation.Name
             
            
            });



            return result;
        }

        public Question GetById(Guid id)
        {

            return _repoQuestion.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
        }
        #endregion
    }
}
