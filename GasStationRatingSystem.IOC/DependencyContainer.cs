using GasStationRatingSystem.BLL;
using GasStationRatingSystem.BLL.People;
using GasStationRatingSystem.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GasStationRatingSystem.IOC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region DAL

            services.AddScoped(typeof(IUnitOfWork<GasStationRatingSystemContext>), typeof(UnitOfWork<GasStationRatingSystemContext>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
               services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            #region BLL
            services.AddScoped<UserBll>();
            services.AddScoped<SendBll>();
            #endregion

        }
    }
}
