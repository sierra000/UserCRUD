using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserCRUD.Core.Extensions;
using UserCRUD.Core.Interfaces;
using UserCRUD.Core.Interfaces.Service;
using UserCRUD.Core.Model;
using UserCRUD.DTO.v1.API;
using UserCRUD.DTO.v1.API.Enums;

namespace UserCRUD.Core.Services
{
    public abstract class BaseService<T, W> : IBaseService<T, W> where T : IEntityDTO, new() where W : IEntity
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseService<T, W>> _logger;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseService<T, W>> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public virtual async Task<ResponseAPIDTO<T>> Get(long id)
        {
            try
            {
                using (_unitOfWork)
                {
                    var repo = _unitOfWork.GetRepositoryFactory<W>();

                    var entity = await repo.GetById(id);

                    if (entity != null)
                        return new ResponseAPIDTO<T>()
                        {
                            Response = _mapper.Map<T>(entity),
                            StatusCode = HttpStatusCode.OK,
                            Succeed = true
                        };

                    return new ResponseAPIDTO<T>()
                    {
                        Response = _mapper.Map<T>(entity),
                        StatusCode = HttpStatusCode.BadRequest,
                        Succeed = false,
                        Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.EntityNotFound, Message = $"Entity not found {typeof(T).ToString()}" } }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error getting entity in BaseService.GetAll(id = {id})");

                return FillError();
            }
        }

        public async Task<ResponseAPIDTO<IEnumerable<T>>> GetAll()
        {
            try
            {
                _logger.CustomLogInformation("Getting all entities in BaseService.GetAll()");

                using (_unitOfWork)
                {
                    var repo = _unitOfWork.GetRepositoryFactory<W>();

                    var entities = await repo.GetAll();

                    return FillResponse(entities);
                }
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, "Error getting all entities in BaseService.GetAll()");

                return FillEnumerableErrors();
            }
        }

        public async Task<ResponseAPIDTO<IEnumerable<T>>> GetAll(int page, int size)
        {
            try
            {
                _logger.CustomLogInformation($"Getting all entities in BaseService.GetAll(page = {page}, size = {size})");

                using (_unitOfWork)
                {
                    var repo = _unitOfWork.GetRepositoryFactory<W>();

                    var entities = await repo.GetAllPaged(page, size);

                    return FillResponse(entities);
                }
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, "Error getting entities in BaseService.GetAll(page = {page}, size = {size})");

                return FillEnumerableErrors();
            }
        }

        public async Task<ResponseAPIDTO<T>> Create(T entityDTO)
        {
            try
            {
                using (_unitOfWork)
                {
                    var repo = _unitOfWork.GetRepositoryFactory<W>();

                    var entity = _mapper.Map<W>(entityDTO);

                    entity = await repo.Create(entity);

                    if (entity != null && await _unitOfWork.Save() > 0)
                    {
                        return new ResponseAPIDTO<T>()
                        {
                            Response = _mapper.Map<T>(entity),
                            StatusCode = HttpStatusCode.OK,
                            Succeed = true
                        };
                    }

                    return new ResponseAPIDTO<T>()
                    {
                        Response = _mapper.Map<T>(entity),
                        StatusCode = HttpStatusCode.BadRequest,
                        Succeed = false,
                        Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.CreateEntityError, Message = $"Error creating entity {typeof(T).ToString()}" } }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error creating entity in BaseService.Create(entity = {typeof(T).ToString()})");

                return FillError();
            }
        }

        public async Task<ResponseAPIDTO<T>> Update(T entityDTO)
        {
            try
            {
                using (_unitOfWork)
                {
                    var repo = _unitOfWork.GetRepositoryFactory<W>();

                    var entity = _mapper.Map<W>(entityDTO);

                    entity = repo.Update(entity);
                    if (entity != null && await _unitOfWork.Save() > 0)
                    {
                        return new ResponseAPIDTO<T>()
                        {
                            Response = _mapper.Map<T>(entity),
                            StatusCode = HttpStatusCode.OK,
                            Succeed = true
                        };
                    }

                    return new ResponseAPIDTO<T>()
                    {
                        Response = _mapper.Map<T>(entity),
                        StatusCode = HttpStatusCode.BadRequest,
                        Succeed = false,
                        Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UpdateEntityError, Message = $"Error updating entity {typeof(T).Name.ToString()}" } }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error updating entity in BaseController.Update(language = {typeof(T).Name.ToString()})");

                return FillError();
            }
        }

        public async Task<ResponseAPIDTO<bool>> Delete(T entityDTO)
        {
            try
            {
                using (_unitOfWork)
                {
                    var repo = _unitOfWork.GetRepositoryFactory<W>();

                    var entity = _mapper.Map<W>(entityDTO);

                    entity = repo.Delete(entity);
                    if (entity != null && await _unitOfWork.Save() > 0)
                    {

                        return new ResponseAPIDTO<bool>()
                        {
                            Response = true,
                            StatusCode = HttpStatusCode.OK,
                            Succeed = true
                        };
                    }

                    return new ResponseAPIDTO<bool>()
                    {
                        Response = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Succeed = false,
                        Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.DeleteEntityError, Message = $"Error deleting entity {typeof(T).Name}" } }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error deleting entity in BaseController.Delete(language = {typeof(T).Name}");

                return new ResponseAPIDTO<bool>()
                {
                    Response = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                };
            }
        }

        public async Task<ResponseAPIDTO<bool>> Delete(long id)
        {
            try
            {
                using (_unitOfWork)
                {
                    var repo = _unitOfWork.GetRepositoryFactory<W>();

                    var entity = await repo.Delete(id);
                    if (entity != null && await _unitOfWork.Save() > 0)
                    {
                        return new ResponseAPIDTO<bool>()
                        {
                            Response = true,
                            StatusCode = HttpStatusCode.OK,
                            Succeed = true
                        };
                    }

                    return new ResponseAPIDTO<bool>()
                    {
                        Response = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Succeed = false,
                        Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.DeleteEntityError, Message = $"Error deleting entity {id}" } }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error deleting entity in BaseController.Delete(id = {id}");

                return new ResponseAPIDTO<bool>()
                {
                    Response = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Succeed = false,
                    Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
                };
            }
        }

        protected ResponseAPIDTO<T> FillError(string message = null)
        {
            return new ResponseAPIDTO<T>()
            {
                Response = (T)Activator.CreateInstance(typeof(T)),
                StatusCode = HttpStatusCode.BadRequest,
                Succeed = false,
                Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = string.IsNullOrEmpty(message) ? "Internal Error" : message } }
            };
        }

        private ResponseAPIDTO<IEnumerable<T>> FillEnumerableErrors()
        {
            return new ResponseAPIDTO<IEnumerable<T>>()
            {
                Response = new List<T>(),
                StatusCode = HttpStatusCode.BadRequest,
                Succeed = false,
                Errors = new List<ErrorAPIDTO>() { new ErrorAPIDTO() { ErrorAPIType = ErrorAPIType.UnexpectedError, Message = "Internal Error" } }
            };
        }

        private ResponseAPIDTO<IEnumerable<T>> FillResponse(IList<W> entities)
        {
            return new ResponseAPIDTO<IEnumerable<T>>()
            {
                Response = entities.Select(_mapper.Map<T>).ToList(),
                Succeed = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
