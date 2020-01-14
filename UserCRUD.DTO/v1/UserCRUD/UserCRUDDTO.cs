using System;
using System.Collections.Generic;
using System.Text;
using UserCRUD.DTO.v1.API;

namespace UserCRUD.DTO.v1.UserCRUD
{
    public class UserCRUDDTO : IEntityDTO
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
