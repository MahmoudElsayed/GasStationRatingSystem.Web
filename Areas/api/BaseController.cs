using GasStationRatingSystem.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Web.Areas.api
{
    [Area(AppConstants.Areas.api)]
    [ApiController]
    [Route("[area]/[controller]/[action]")]
    public class BaseController : Controller
    {
        
    }
}
