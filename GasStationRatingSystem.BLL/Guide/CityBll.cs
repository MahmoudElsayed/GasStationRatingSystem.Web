using GasStationRatingSystem.DAL;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{

   public class CityBll
    {
        #region Fields

        private readonly IRepository<City> _repoCity;
        private readonly IRepository<District> _repoDistrict;


        public CityBll(IRepository<City> repoCity, IRepository<District> repoDistrict)
        {
            _repoCity = repoCity;
            _repoDistrict = repoDistrict;
        }

        #endregion
        #region GetCities
        public IEnumerable<City> GetCities() => _repoCity.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted);

        #endregion
        #region GetDistricts
        public IEnumerable<District> GetDistricts(Guid cityId) => _repoDistrict.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted&&p.CityId==cityId);

        #endregion
    }
}
