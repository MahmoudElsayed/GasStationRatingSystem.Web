using GasStationRatingSystem.BLL;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Web.Areas.api
{
    public class VisitController : BaseController
    {
        #region Fields

        private readonly GasStationBll _gasStationBll;
        private readonly UserBll _userBll;
        private readonly VisitBll _visitBll;

        public VisitController(GasStationBll gasStationBll, UserBll userBll, VisitBll visitBll)
        {
            _gasStationBll = gasStationBll;
            _userBll = userBll;
            _visitBll = visitBll;
        }
        #endregion
        [HttpGet]
        public IActionResult GetSations() => StatusCode(AppConstants.StatusCodes.Success, _gasStationBll.GetStaions());
        #region GetUsers
        [HttpGet]
        public IActionResult GetUsers()
        {

            return  StatusCode(AppConstants.StatusCodes.Success, _userBll.Get()); 
        }

        #endregion
        #region Create Visit
        [HttpPost]
        public IActionResult CreateVisit(CreateVisitDTO para)
        {
            if (!ModelState.IsValid)
            {
                var result = new ResultDTO();
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                result.Message = HttpContext.GetLocalizedString(errors[0].FirstOrDefault().ErrorMessage);
                return StatusCode(AppConstants.StatusCodes.Error, result);

            }
            var data = _visitBll.Create(para);
           
                return StatusCode(data.data!=null?AppConstants.StatusCodes.Success: AppConstants.StatusCodes.Error, data);
            
           

        }
        #endregion
        [HttpPost]
        public IActionResult SaveAnswers(SaveVisitAnswersDTO para)
        {
            if (!ModelState.IsValid)
            {
                var result = new ResultDTO();
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                result.Message = HttpContext.GetLocalizedString(errors[0].FirstOrDefault().ErrorMessage);
                return StatusCode(AppConstants.StatusCodes.Error, result);

            }
            var data = _visitBll.SaveVisitAnswer(para);

            return StatusCode(data.data != null ? AppConstants.StatusCodes.Success : AppConstants.StatusCodes.Error, data);
        }

        [HttpPost]
        public IActionResult SaveOffLineAnswers(SaveOfflineVisitAnswersDTO para)
        {
            var result = new ResultDTO();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                result.Message = HttpContext.GetLocalizedString(errors[0].FirstOrDefault().ErrorMessage);
                return StatusCode(AppConstants.StatusCodes.Error, result);

            }
             para.SaveVisitsAnswers.ForEach( x=> _visitBll.SaveVisitAnswer(x));
            result.data =new { };
            return StatusCode(result.data != null ? AppConstants.StatusCodes.Success : AppConstants.StatusCodes.Error, result);
        }
    }
}
