using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserCRUD.Core.Extensions;
using UserCRUD.Core.Interfaces;
using UserCRUD.Core.Interfaces.Repository;
using UserCRUD.Core.Interfaces.Repository.UserCRUD;
using UserCRUD.Core.Model;

namespace UserCRUD.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly UserCRUDDbContext _dbContext;
        readonly ILogger _logger;
        readonly IServiceProvider _serviceProvider;

        Lazy<IUserCRUDRepository> _userCRUDRepository;

        public UnitOfWork(UserCRUDDbContext dbContext, ILogger<UnitOfWork> logger, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> Save()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, "Error saving dbContext");
            }

            return -1;
        }

        public virtual void Dispose()
        {
            try
            {
                _dbContext.Dispose();
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, "Error disposing dbContext");
            }
        }

        public IUserCRUDRepository UserCRUDRepository
        {
            get
            {
                if (_userCRUDRepository == null)
                    _userCRUDRepository = new Lazy<IUserCRUDRepository>(() => (IUserCRUDRepository)_serviceProvider.GetService(typeof(IUserCRUDRepository)));

                return _userCRUDRepository.Value;
            }
        }

        public IBaseRepository<T> GetRepositoryFactory<T>() where T : IEntity => (IBaseRepository<T>)_serviceProvider.GetService(typeof(IBaseRepository<T>));

    }
}
