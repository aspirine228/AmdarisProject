using GameTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameTracker.Common.Models.PagedRequest;

namespace GameTracker.Rep.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<TEntity> GetByIdWithInclude<TEntity>(int id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity;
        Task<List<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties);
      
        Task<T> GetById(int id);
        Task<T> TryGetById(int id);
        List<T> GetAll();
        Task SaveChangesAsync();
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        Task Add(T user);
        Task Update(T user);
        Task<T> Remove(int id);
        Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : BaseEntity
                                                                                           where TDto : class;
    }
}
