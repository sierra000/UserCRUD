using System;
using System.Collections.Generic;
using System.Text;

namespace UserCRUD.Core.Model.UserCRUD
{
    public class UserCRUD : IEntity
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
