using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class GasStationBll
    {
        #region Fields

        private readonly IRepository<GasStation> _repoGasStation;


        public GasStationBll(IRepository<GasStation> repoGasStation)
        {
            _repoGasStation = repoGasStation;
        }

        #endregion
        #region Get
        public ResultDTO Get()
        {
            ResultDTO result = new ResultDTO();
            var data = _repoGasStation.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted).Select(p=>new { 
            Id=p.ID, Name=p.Name
            });
            result.data = new {Stations=data };
            return result;
        }
        public GasStation GetById(Guid id)
        {

            return _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
        }
        #endregion
    }
}
