using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using UserCRUD.Core.Interfaces.Repository.UserCRUD;

namespace UserCRUD.Infrastructure.Repository.UserCRUD
{
    public class UserCRUDRepository : BaseRepository<Core.Model.UserCRUD.UserCRUD>, IUserCRUDRepository
    {
        public UserCRUDRepository(UserCRUDDbContext dbContext, ILogger<BaseRepository<Core.Model.UserCRUD.UserCRUD>> logger) : base(dbContext, logger)
        {
        }
    }
}
