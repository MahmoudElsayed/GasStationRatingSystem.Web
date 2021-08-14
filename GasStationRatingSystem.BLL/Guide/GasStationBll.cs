using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class GasStationBll
    {
        #region Fields
        private const string _spGasStations = "Guide.[spGasStations]";

        private readonly IRepository<GasStation> _repoGasStation;

        private readonly ManualDistributionBll _manualDistributionBll;
        public GasStationBll(IRepository<GasStation> repoGasStation, ManualDistributionBll manualDistributionBll)
        {
            _repoGasStation = repoGasStation;
            _manualDistributionBll = manualDistributionBll;
        }

        #endregion
        #region Get
        public ResultDTO Get()
        {
            ResultDTO result = new ResultDTO();
            var data = _repoGasStation.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted).Select(p=>new { 
            Id=p.ID, Name=p.NameInLicense.TrimEnd()
                ,Latitude=p.Lat
                ,Longitude=p.Lng
            });;
            result.data = new {Stations=data };
            return result;
        }
        public ResultDTO GetStaions()
        {
          
            return _manualDistributionBll.GetStaions();
        }
        public GasStation GetById(Guid id)
        {

            return _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
        }
        public IEnumerable<GasStation> GetStaions(Guid districtId)
        {

            return _repoGasStation.GetAllAsNoTracking().Where(p => p.DistrictId == districtId);
        }
        #endregion
        public ResultViewModel Save(GasStationDTO GasStationDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == GasStationDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoGasStation.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == GasStationDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var config = new MapperConfiguration(p => p.CreateMap<GasStationDTO, GasStation>());
                var mapper = new Mapper(config);
                var tbl = mapper.Map<GasStation>(GasStationDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoGasStation.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoGasStation.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == GasStationDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var config = new MapperConfiguration(p => p.CreateMap<GasStationDTO, GasStation>());
                var mapper = new Mapper(config);
                var tbl = mapper.Map<GasStation>(GasStationDTO);

                if (_repoGasStation.Insert(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }





            return resultViewModel;
        }

        public ResultViewModel Delete(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoGasStation.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }




        #region LoadData
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoGasStation.ExecuteStoredProcedure<GasStationDTO>
                (_spGasStations, mdl?.ToSqlParameter(_repoGasStation.UserId), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
      
        #endregion
    }
}
