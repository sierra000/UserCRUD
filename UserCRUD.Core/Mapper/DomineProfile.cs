using AutoMapper;
using System.Security.Claims;
using UserCRUD.DTO.v1.UserCRUD;

namespace UserCRUD.Core.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            #region UserCRUD
            
            CreateMap<UserCRUDDTO, Model.UserCRUD.UserCRUD>();
            CreateMap<Model.UserCRUD.UserCRUD, UserCRUDDTO>();
            

            #endregion
        }
    }
}
