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
   public class UserBll
    {
        #region Fields
        private const string _spUsers = "People.[spUsers]";

        private readonly IRepository<User> _repoUser;


        public UserBll(IRepository<User> repoUser)
        {
            _repoUser = repoUser;
        }

        #endregion
        #region Get
        public ResultDTO Get()
        {
            ResultDTO result = new ResultDTO();
            result.data = new
            {
                Users = _repoUser.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted && p.ID != _repoUser.UserId).Select(p => new
                {
                    Id = p.ID,
                    Name = p.UserName
                })
            };
            return result;
        }

        public User GetById(Guid id)
        {
           
            return _repoUser.GetAllAsNoTracking().Where(p=>p.ID==id).FirstOrDefault();
        }
        #endregion

        #region Login
        public ResultDTO Login(UserParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAll().Where(p => (p.UserName.ToLower() == para.Username.ToLower() || p.Email.ToLower() == para.Username.ToLower()) && p.PasswordHash == para.Password.EncryptString()).FirstOrDefault();
            if (data != null)
            {

                result.data = new { User = new UserResultDTO { Id = data.ID, Username = data.UserName, UseDefaultPassword= data.UseDefaultPassword } };
            }
            else
            {
             result.Message=   _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorInUsernameOrPassword));
            }
            return result;

        }
        #endregion
        #region Change Password
        public ResultDTO ChangePassword(UserChangePasswordParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAllAsNoTracking().Where(p => p.ID==_repoUser.UserId).FirstOrDefault();
            if (data != null)
            {
                data.PasswordHash = para.NewPassword.EncryptString();
                data.UseDefaultPassword = false;
                if( _repoUser.Update(data))
                {
                    result.data = new { Successed = true };

                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                }
                else
                {
                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                }
            }
            else
            {
                result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));
            }
            return result;

        }
        #endregion
        #region Forget Password
        public ResultDTO SendCode(UserSendCodeParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAll().Where(p => (p.UserName.ToLower() == para.Username.ToLower() || p.Email.ToLower() == para.Username.ToLower()) ).FirstOrDefault();
            if (data != null)
            {
                var _SendBll = _repoUser.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(SendBll)) as SendBll;
                string Code = new Random((int)DateTime.Now.Ticks).Next(11111, 99999)+"";
                if (_SendBll.SendMail("كود تعيين كلمة المرور",data.UserName,$"كود التحقق  {Code}",data.Email))
                {
                    data.ResetPasswordDate = AppDateTime.Now;
                    data.CodeOfReset = Code;
                    if (_repoUser.Update(data))
                    {
                        result.data = new { Successed = true };

                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                    }
                    else
                    {
                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                    }
                }
                else
                {
                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                }
            }
            else
            {
                result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));
            }
            return result;

        }
        public ResultDTO ForgetPassword(UserForgetPasswordParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAll().Where(p => (p.UserName.ToLower() == para.Username.ToLower() || p.Email.ToLower() == para.Username.ToLower())).FirstOrDefault();
            if (data != null)
            {
                if (data.CodeOfReset != para.Code)
                {
                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ValidationCodeInCorrect));

                }
                else
                {
                    data.PasswordHash = para.NewPassword.EncryptString();
                    data.CodeOfReset = null;
                    data.ResetPasswordDate = null;
                    if (_repoUser.Update(data))
                    {
                        result.data = new { Successed = true };

                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                    }
                    else
                    {
                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                    }
                }
            }
            else
            {
                result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));
            }
            return result;

        }

        #endregion

        #region Web
        public ResultViewModel Save(UserDTO userDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };
            var config = new MapperConfiguration(p => p.CreateMap<UserDTO, User>());
            var mapper = new Mapper(config);
            var user = mapper.Map<User>(userDTO);
            var tbl = _repoUser.GetById(user.ID);
            if (tbl == null)
            {
                if (_repoUser.GetAll().Where(p => p.UserName.Trim().ToLower() == userDTO.UserName.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.UsernameAlreadyExists;
                    return resultViewModel;

                }

                if (_repoUser.GetAll().Where(p => p.Email.Trim().ToLower() == userDTO.Email.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.EmailAlreadyExists;
                    return resultViewModel;
                }

                user.PasswordHash = AppConstants.DefaultPassword.EncryptString();
                user.Salt = AppConstants.EncryptKey;

                if (_repoUser.Insert(user))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoUser.GetAll().Where(p => p.ID != tbl.ID && p.UserName.Trim().ToLower() == userDTO.UserName.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.UsernameAlreadyExists;
                    return resultViewModel;

                }

                if (_repoUser.GetAll().Where(p => p.ID != tbl.ID && p.Email.Trim().ToLower() == userDTO.Email.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.EmailAlreadyExists;
                    return resultViewModel;
                }

                tbl.UserName = user.UserName;
                tbl.Email = user.Email;
                tbl.IsActive = user.IsActive;
                tbl.CityId = user.CityId;
                if (_repoUser.Update(tbl))
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
            var tbl = _repoUser.GetById(id);
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoUser.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }
        public ResultViewModel ChangeStatus(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoUser.GetById(id);
            tbl.IsActive = !tbl.IsActive;
            var IsSuceess = _repoUser.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.ChangedStatusSuccess : AppConstants.Messages.ChangedStatusFailed;


            return resultViewModel;
        }

        #region LoadData
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoUser.ExecuteStoredProcedure<UserDTO>
                (_spUsers, mdl?.ToSqlParameter(_repoUser.UserId), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        #region Login For Web
        #region  تسجيل الدخول
        public User LogInWeb(LogInDTO mdl)
        {
            if (mdl != null && !mdl.UserCode.IsEmpty() && !mdl.Password.IsEmpty())
            {
                var pass = mdl.Password.EncryptString();
                var user = _repoUser.GetAll().Where(c => c.IsActive == true && !c.IsDeleted && (c.UserName == mdl.UserCode || c.Email == mdl.UserCode) && c.PasswordHash == pass).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
            }

            return null;
        }
        #endregion
        #region تغيير كلمة المرور
        public ResultViewModel ChangeOldPasswordWeb(ChangePasswordDTO mdl)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Status = false, Message = AppConstants.Messages.SavedFailed };
            if (mdl != null)
            {
                var currentUser = _repoUser.GetById(_repoUser.UserId);
                if (currentUser != null)
                {
                    var hashPass = mdl.OldPassword.EncryptString();
                    if (hashPass == currentUser.PasswordHash)
                    {
                        var hashNewPass = (mdl.NewPassword).EncryptString();
                        if (hashNewPass != null)
                        {
                            currentUser.PasswordHash = hashNewPass;
                            if (_repoUser.Update(currentUser))
                            {
                                resultViewModel.Status = true;
                                resultViewModel.Message = AppConstants.Messages.SavedSuccess;
                            }
                        }
                    }
                }
            }
            return resultViewModel;
        }

        #endregion

        #endregion
        #endregion
        #region GetUsers
        public IEnumerable<User> GetUsers(Guid cityId) => _repoUser.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted&&p.CityId==cityId);

        #endregion
    }
}
