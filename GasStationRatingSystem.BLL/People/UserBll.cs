using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class UserBll
    {
        #region Fields

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
    }
}
