using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserCRUD.Core.Extensions;
using UserCRUD.Core.Interfaces.Repository;
using UserCRUD.Core.Model;

namespace UserCRUD.Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : IEntity
    {
        protected ILogger _logger;
        protected UserCRUDDbContext _dbContext;

        protected string _entityName;

        protected BaseRepository(UserCRUDDbContext dbContext, ILogger<BaseRepository<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;

            _entityName = typeof(T).Name;
        }

        void IBaseRepository<T>.DbContext(object _dbContext) => _dbContext = (UserCRUDDbContext)_dbContext;

        public virtual async Task<T> GetById(long id) => await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        public virtual async Task<IList<T>> GetAll() => await _dbContext.Set<T>().ToListAsync();
        public virtual async Task<IList<T>> GetAllPaged(int page = 0, int size = 10) => await _dbContext.Set<T>().Skip(page * size).Take(size).ToListAsync();
        public virtual async Task<IList<T>> GetAllFiltered(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, bool? ascending, int page = 0, int size = 10)
        {
            try
            {
                var entitiesFiltered = _dbContext.Set<T>().Where(where);

                if (order != null)
                {
                    if (ascending.HasValue && ascending.Value)
                        entitiesFiltered = entitiesFiltered.OrderBy(order);
                    else if (ascending.HasValue && !ascending.Value)
                        entitiesFiltered = entitiesFiltered.OrderByDescending(order);
                }

                if (size == 0)
                    size = 10;

                return await entitiesFiltered.Skip(page * size).Take(size).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error getting filtered entities of {_entityName} with where expression {where.ToString()}, order expression {order.ToString()}, ascending {ascending.ToString()}, page {page} and size {size}");
            }

            return null;
        }

        public virtual async Task<long> GetCountFiltered(Expression<Func<T, bool>> where)
        {
            try
            {
                var entitiesFiltered = _dbContext.Set<T>().Where(where);

                return await entitiesFiltered.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error getting filtered entities of {_entityName} with where expression {where.ToString()}");
            }

            return 0;
        }

        public virtual async Task<T> Create(T entity)
        {
            try
            {
                var entityEntry = await _dbContext.AddAsync(entity);

                if (entityEntry.State == EntityState.Added)
                    return entityEntry.Entity;

                _logger.CustomLogError($"Entity not created: {_entityName}");
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error creating entity {_entityName}");
            }

            return null;
        }

        public virtual void Create(IEnumerable<T> entities)
        {
            try
            {

                _dbContext.AddRange(entities);

                _logger.CustomLogError($"Entities not created: {_entityName}");
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error creating entities: {_entityName}");
            }
        }

        public virtual T Update(T entity)
        {
            try
            {
                var entityEntry = _dbContext.Update(entity);
                _dbContext.Entry(entity).Property(x => x.Id).IsModified = false;

                if (entityEntry.State == EntityState.Modified)
                    return entityEntry.Entity;

                _logger.CustomLogError($"Entity not updated: {_entityName} with Id: {entity.Id.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error updating entity: {_entityName} with Id: {entity?.Id.ToString()}");
            }

            return null;
        }

        public virtual int Update(IEnumerable<T> entities)
        {
            try
            {
                _dbContext.UpdateRange(entities);
                foreach (var entity in entities)
                {
                    _dbContext.Entry(entity).Property(x => x.Id).IsModified = false;
                }

                return 1;
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error updating entities: {_entityName} with Id: {entities.Select(x => x.Id)}");

                return 0;
            }
        }

        public virtual T Delete(T entity)
        {
            try
            {
                if (entity != null)
                {
                    if (_dbContext.Remove(entity).State == EntityState.Deleted)
                        return entity;
                                      
                }

                _logger.CustomLogError($"Entity not found on delete: {_entityName}");
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error deleting entity: {_entityName} with Id: {entity?.Id}");
            }

            return null;
        }

        public virtual async Task<T> Delete(long id)
        {
            try
            {
                var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                if (entity != null)
                    return Delete(entity);

                _logger.CustomLogError($"Entity not found on delete: {_entityName} with Id: {id}");
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error deleting entity: {_entityName} with Id: {id}");
            }

            return null;
        }

        public virtual int Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {

                    return Update(entities);
                }

                _logger.CustomLogError($"Entities not found on delete: {_entityName}");
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error deleting entities: {_entityName} with Id: {entities.Select(x => x.Id)}");
            }

            return 0;
        }

        public virtual async Task<int> Delete(IEnumerable<long> ids)
        {
            try
            {
                var entities = await _dbContext.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
                if (entities != null && entities.Any())
                    return Delete(entities);

                _logger.CustomLogError($"Entities not found on delete: {_entityName} with Id: {ids}");
            }
            catch (Exception ex)
            {
                _logger.CustomLogError(ex, $"Error deleting entities: {_entityName} with Id: {ids}");
            }

            return 0;
        }
    }
}
