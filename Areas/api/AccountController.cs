using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GasStationRatingSystem.Common.AppConstants;

namespace GasStationRatingSystem.Web.Areas.api
{
    
    public class AccountController : BaseController
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            return StatusCode(StatusCodes.Error, new {data= HttpContext.LanguageIsArabic() });
        }
    }
}
