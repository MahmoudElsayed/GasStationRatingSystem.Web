using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class VisitBll
    {

        #region Fields

        private readonly IRepository<VisitInfo> _repoVisitInfo;
        private readonly IRepository<VisitAnswer> _repoVisitAnswer;

        private readonly UserBll _userBll;
        private readonly GasStationBll _gasStationBll;

        public VisitBll(IRepository<VisitInfo> repoVisitInfo, IRepository<VisitAnswer> repoVisitAnswer, UserBll userBll, GasStationBll gasStationBll)
        {
            _repoVisitInfo = repoVisitInfo;
            _repoVisitAnswer = repoVisitAnswer;
            _userBll = userBll;
            _gasStationBll = gasStationBll;
        }

        #endregion
        #region Create Visit
        public ResultDTO Create(CreateVisitDTO para)
        {
            ResultDTO result = new ResultDTO();
            var user = _userBll.GetById(para.UserId??Guid.NewGuid());
            var station = _gasStationBll.GetById(para.StationId.Value);

            if (para.UserId.HasValue&&user == null)
            {
                result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));

            }
            else if (station == null)
            {
                result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.StationNotExists));

            }
            else
            {
                VisitInfo data = new VisitInfo() { UserId = para.UserId, StationId=para.StationId.Value };
                if (_repoVisitInfo.Insert(data))
                {
                    result.data = new { Visit = new { Id = data.ID, VisitNo = data.VisitNo } };

                    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                }
                else
                {
                    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                }
            }
            return result;
        }
        #endregion
        #region Save Visit Answers
        public ResultDTO SaveVisitAnswer(SaveVisitAnswersDTO para)
        {
            ResultDTO result = new ResultDTO();

            List<VisitAnswerOption> visitAnswerOptions = new List<VisitAnswerOption>();
                VisitAnswer data = new VisitAnswer() { VisitId=para.VisitId.Value, AddedBy=_repoVisitAnswer.UserId  };
            para.Options.ForEach(p => { visitAnswerOptions.Add(new VisitAnswerOption() { AnswerId = p.OptionId, RefId=p.RefId, VisitAnswerId=data.ID }); });
            data.VisitAnswerOptions = visitAnswerOptions;
            if (_repoVisitAnswer.Insert(data))
                {
                result.data = null;

                    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                }
                else
                {
                    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                }
            
            return result;
        }
        #endregion
    }
}
