using GasStationRatingSystem.BLL;
using GasStationRatingSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Web.Controllers
{
    public class HomeController : ParentController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DashboardBll _dashboardBll;  

        public HomeController(ILogger<HomeController> logger, DashboardBll dashboardBll)
        {
            _logger = logger;
            _dashboardBll = dashboardBll;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region Helpers
        public IActionResult LoadTotals() => Ok( _dashboardBll.GetTotals());
        public IActionResult LoadCitiesSations() => Ok(_dashboardBll.GetCitiesSations());

        #endregion
    }
}
