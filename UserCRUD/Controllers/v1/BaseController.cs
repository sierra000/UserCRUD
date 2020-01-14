using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserCRUD.Core.Extensions;
using UserCRUD.Core.Interfaces.Service;
using UserCRUD.DTO.v1.API;
using UserCRUD.DTO.v1.API.Enums;

namespace UserCRUD.Controllers.v1
{
    public abstract class BaseController<T> : Controller where T : IEntityDTO
    {
        public const string _controllerName = "";

        protected IBaseService<T> _baseService;

        protected BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        #region BasicCRUD

        [HttpGet]
        [Authorize]
        public virtual async Task<IActionResult> GetAll()
        {
            try
            {

                return Json(await _baseService.GetAll());
            }
            catch (Exception ex)
            {

                return Json(new ResponseAPIDTO<IEnumerable<T>>()
                {
                    Response = new List<T>(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                });
            }
        }

        [HttpGet]
        [Authorize]
        public virtual async Task<IActionResult> Get(int page, int size)
        {
            try
            {
           
                return Json(await _baseService.GetAll(page, size));
            }
            catch (Exception ex)
            {
                return Json(new ResponseAPIDTO<IEnumerable<T>>()
                {
                    Response = new List<T>(),
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                });
            }
        }

        [HttpGet]
        [Authorize]
        public virtual async Task<IActionResult> Get(long id)
        {
            try
            {

                return Json(await _baseService.Get(id));
            }
            catch (Exception ex)
            {

                return Json(new ResponseAPIDTO<T>()
                {
                    Response = (T)Activator.CreateInstance(typeof(T)),
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                });
            }
        }

        [HttpPost]
        [Authorize]
        public virtual async Task<IActionResult> Create([FromBody]T entity)
        {
            try
            {

                return Json(await _baseService.Create(entity));
            }
            catch (Exception ex)
            {
                return Json(new ResponseAPIDTO<T>()
                {
                    Response = (T)Activator.CreateInstance(typeof(T)),
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                });
            }
        }

        [HttpPut]
        [Authorize]
        public virtual async Task<IActionResult> Update([FromBody]T entityDTO)
        {
            try
            {
                return Json(await _baseService.Update(entityDTO));
            }
            catch (Exception ex)
            {
                return Json(new ResponseAPIDTO<T>()
                {
                    Response = (T)Activator.CreateInstance(typeof(T)),
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                });
            }
        }

        [HttpDelete]
        [Authorize]
        public virtual async Task<IActionResult> Delete(long id)
        {
            try
            {
                return Json(await _baseService.Delete(id));
            }
            catch (Exception ex)
            {
                return Json(new ResponseAPIDTO<T>()
                {
                    Response = (T)Activator.CreateInstance(typeof(T)),
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                });
            }
        }

        #endregion
    }
}