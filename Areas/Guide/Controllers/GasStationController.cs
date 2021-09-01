using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.AspNetCore.Mvc;

namespace GasStationRatingSystem.Web.Areas.Guide
{
    public class GasStationController : Controller
    {
        #region Fields
        private readonly GasStationBll _GasStationBll;
        public GasStationController(GasStationBll GasStationBll)
        {
            _GasStationBll = GasStationBll;
        }
        #endregion
        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View(new GasStationDTO());

        }
        public IActionResult Save(GasStationDTO mdl) => Ok(_GasStationBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {
            var data = _GasStationBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var config = new MapperConfiguration(p => p.CreateMap<GasStation, GasStationDTO>());
                var mapper = new Mapper(config);
                var tbl = mapper.Map<GasStationDTO>(data);

                return View(tbl);

            }

            return View(new GasStationDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_GasStationBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_GasStationBll.LoadData(mdl));

        #endregion

    }
}
