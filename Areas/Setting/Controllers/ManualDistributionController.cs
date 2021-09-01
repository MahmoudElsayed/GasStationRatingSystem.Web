using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasStationRatingSystem.Web.Areas.Setting.Controllers
{
    public class ManualDistributionController : Controller
    {
        #region Fields

        private readonly UserBll _userBll;
        private readonly CityBll _cityBll;
        private readonly GasStationBll _gasStationBll;
        private readonly ManualDistributionBll _manualDistributionBll;


        public ManualDistributionController(UserBll userBll, CityBll cityBll, GasStationBll gasStationBll, ManualDistributionBll manualDistributionBll)
        {
            _userBll = userBll;
            _cityBll = cityBll;
            _gasStationBll = gasStationBll;
            _manualDistributionBll = manualDistributionBll;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Save(List< ManualDistributionDTO> mdl) => Ok(_manualDistributionBll.Save(mdl));
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_manualDistributionBll.Delete(id));
        [HttpPost]
        public IActionResult ChangeStatus(Guid id) => Ok(_manualDistributionBll.ChangeStatus(id));

        
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_manualDistributionBll.LoadData(mdl));




        #endregion
        #region Helpers
        [HttpPost]
        public IActionResult LoadCities(Guid cityId) => Ok(_cityBll.GetCities());

        
        [HttpPost]
        public IActionResult LoadDistricts(Guid cityId) => Ok(_cityBll.GetDistricts(cityId));
        [HttpPost]
        public IActionResult LoadUsers(Guid cityId) => Ok(_userBll.GetUsers(cityId));
        [HttpPost]
        public IActionResult LoadStaions(Guid districtId) => Ok(_gasStationBll.GetStaions(districtId));

        
        #endregion
    }
}
