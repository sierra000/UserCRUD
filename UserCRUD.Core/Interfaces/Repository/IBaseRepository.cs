using System.Collections.Generic;
using System.Threading.Tasks;
using UserCRUD.Core.Model;

namespace UserCRUD.Core.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : IEntity
    {
        void DbContext(object _dbContext);
        Task<T> GetById(long id);
        Task<IList<T>> GetAll();
        Task<IList<T>> GetAllPaged(int page = 0, int size = 10);
        Task<T> Create(T entity);
        void Create(IEnumerable<T> entities);
        T Update(T entity);
        int Update(IEnumerable<T> entities);
        Task<T> Delete(long id);
        Task<int> Delete(IEnumerable<long> ids);
        T Delete(T entity);
        int Delete(IEnumerable<T> entities);
    }
}
