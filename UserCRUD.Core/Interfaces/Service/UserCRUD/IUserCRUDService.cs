using System;
using System.Collections.Generic;
using System.Text;
using UserCRUD.DTO.v1.UserCRUD;

namespace UserCRUD.Core.Interfaces.Service.UserCRUD
{
    public interface IUserCRUDService : IBaseService<UserCRUDDTO, Model.UserCRUD.UserCRUD>
    {
    }
}
