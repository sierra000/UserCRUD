using System.Collections.Generic;
using System.Threading.Tasks;
using UserCRUD.Core.Model;
using UserCRUD.DTO.v1.API;

namespace UserCRUD.Core.Interfaces.Service
{
    public interface IBaseService<T, W> : IBaseService<T> where T : IEntityDTO, new() where W : IEntity
    {
    }

    public interface IBaseService<T> where T : IEntityDTO
    {
        Task<ResponseAPIDTO<T>> Get(long id);
        Task<ResponseAPIDTO<IEnumerable<T>>> GetAll();
        Task<ResponseAPIDTO<IEnumerable<T>>> GetAll(int page, int size);
        Task<ResponseAPIDTO<T>> Create(T entityDTO);
        Task<ResponseAPIDTO<T>> Update(T entityDTO);
        Task<ResponseAPIDTO<bool>> Delete(T entityDTO);
        Task<ResponseAPIDTO<bool>> Delete(long id);
    }
}
