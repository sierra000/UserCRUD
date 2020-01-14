using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserCRUD.Core.Interfaces;
using UserCRUD.Core.Interfaces.Repository;
using UserCRUD.Core.Interfaces.Repository.UserCRUD;
using UserCRUD.Infrastructure.Repository.UserCRUD;

namespace UserCRUD.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration, string contentRootPath)
        {
            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
            if (connectionString.Contains("%CONTENTROOTPATH%"))
            {
                connectionString = connectionString.Replace("%CONTENTROOTPATH%", contentRootPath);
            }
            services.AddDbContext<UserCRUDDbContext>(options => options.UseSqlServer(connectionString));


            #region UserCRUD

            services.AddScoped(typeof(IUserCRUDRepository), typeof(UserCRUDRepository));
            services.AddScoped(typeof(IBaseRepository<Core.Model.UserCRUD.UserCRUD>), typeof(UserCRUDRepository));

            #endregion

          
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            services.AddEntityFrameworkSqlServer()
            .AddDbContext<UserCRUDDbContext>();
        }

    }
}
