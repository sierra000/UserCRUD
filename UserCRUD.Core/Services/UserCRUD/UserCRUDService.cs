using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserCRUD.Core.Interfaces;
using UserCRUD.Core.Interfaces.Service.UserCRUD;
using UserCRUD.DTO.v1.UserCRUD;

namespace UserCRUD.Core.Services.UserCRUD
{
    public class UserCRUDService : BaseService<UserCRUDDTO, Model.UserCRUD.UserCRUD>, IUserCRUDService
    {
        public UserCRUDService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseService<UserCRUDDTO, Model.UserCRUD.UserCRUD>> logger, IConfiguration configuration) : base(unitOfWork, mapper, logger)
        {
        }
    }
}
