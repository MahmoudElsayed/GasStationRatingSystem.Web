using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasStationRatingSystem.Web.Areas.People.Controllers
{
    public class UserController : Controller
    {
        #region Fields

        private readonly UserBll _userBll;
        private readonly CityBll _cityBll;

        public UserController(UserBll userBll, CityBll cityBll)
        {
            _userBll = userBll;
            _cityBll = cityBll;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            ViewBag.CityId =new SelectList(_cityBll.GetCities(),"ID","Name");
            return View();
        }
        public IActionResult Save(UserDTO mdl) => Ok( _userBll.Save(mdl));
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_userBll.Delete(id));
        [HttpPost]
        public IActionResult ChangeStatus(Guid id) => Ok(_userBll.ChangeStatus(id));

        
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_userBll.LoadData(mdl));

        
      

        #endregion

    }
}
