using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.DependencyInjection;
using UserCRUD.Core.Interfaces.Service.UserCRUD;
using UserCRUD.Core.Services.UserCRUD;

namespace UserCRUD.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddExpressionMapping());

            #region UserCRUD

            services.AddScoped(typeof(IUserCRUDService), typeof(UserCRUDService));

            #endregion

        }
    }
}
