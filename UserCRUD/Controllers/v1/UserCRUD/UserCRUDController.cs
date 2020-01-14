using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserCRUD.Core.Interfaces.Service.UserCRUD;
using UserCRUD.DTO.v1.UserCRUD;

namespace UserCRUD.Controllers.v1.UserCRUD
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserCRUDController : BaseController<UserCRUDDTO>
    {

        public UserCRUDController(IUserCRUDService userCRUDService) : base(userCRUDService)
        {

        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public override async Task<IActionResult> Get(long id)
        {
            return await base.Get(id);
        }

        [HttpGet]
        [AllowAnonymous]
        public override async Task<IActionResult> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("paged/{page}/{size}")]
        [AllowAnonymous]
        public override async Task<IActionResult> Get(int page, int size)
        {
            return await base.Get(page, size);
        }

        [HttpPost]
        [AllowAnonymous]
        public override async Task<IActionResult> Create([FromBody]UserCRUDDTO entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        [AllowAnonymous]
        public override async Task<IActionResult> Update([FromBody]UserCRUDDTO entityDTO)
        {
            return await base.Update(entityDTO);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public override async Task<IActionResult> Delete(long id)
        {
            return await base.Delete(id);
        }

    }
}