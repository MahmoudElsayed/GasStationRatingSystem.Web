using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasStationRatingSystem.Web.Areas.Report.Controllers
{
    public class GetVisitInfoController : Controller
    {
        

        #region Actions
        public IActionResult Index()
        {
            return View();
        }
       
        

        
      

        #endregion

    }
}
