using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class DashboardBll
    {
        #region Fields
        private readonly IRepository<VisitInfo> _repoVisitInfo;
        private const string _spGetTotals= "Dashboard.GetTotals";
        private const string _spGetCitiesSations = "Dashboard.GetCitiesSations";

        
        public DashboardBll(IRepository<VisitInfo> repoVisitInfo)
        {
            _repoVisitInfo = repoVisitInfo;
        }
        #endregion
        #region Helpers
        public DashboardGetTotalsDTO GetTotals()
        {
            return _repoVisitInfo.ExecuteStoredProcedure<DashboardGetTotalsDTO>(_spGetTotals, null)?.FirstOrDefault() ?? new DashboardGetTotalsDTO();
        }
        public  List<DashboardGetTotalsDTO> GetCitiesSations()
        {
            return _repoVisitInfo.ExecuteStoredProcedure<DashboardGetTotalsDTO>(_spGetCitiesSations, null);
        }
        #endregion
    }
}
