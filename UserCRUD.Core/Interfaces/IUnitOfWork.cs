using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserCRUD.Core.Interfaces.Repository;
using UserCRUD.Core.Interfaces.Repository.UserCRUD;
using UserCRUD.Core.Model;

namespace UserCRUD.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> GetRepositoryFactory<T>() where T : IEntity;

        IUserCRUDRepository UserCRUDRepository { get; }


        Task<int> Save();
    }
}
