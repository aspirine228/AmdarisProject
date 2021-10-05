
using GameTracker.Domain.Entities;
using GameTracker.Rep.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System.Linq.Expressions;
using GameTracker.Common.Models.PagedRequest;
using GameTracker.Rep.Extensions;
using GameTracker.Common.Exceptions;

namespace GameTracker.Rep.Repositories
{
    public class BaseRepository<T> : IDisposable ,IRepository<T> where T : BaseEntity
    {
        private readonly MyGameDbContext _dbContext;
        private readonly IMapper _mapper;

       
        public BaseRepository(MyGameDbContext dbContext, IMapper mapper)
        {          
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            DbSet<T> _set = _dbContext.Set<T>();
            return _set.Where(predicate);
        }
        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();//_dbContext.(x => x.Id);
        }
  

        public async Task<List<T>> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = IncludeProperties(includeProperties);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdWithInclude<TEntity>(int id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : BaseEntity
                                                                                                  where TDto : class
        {
            return await _dbContext.Set<TEntity>().CreatePaginatedResultAsync<TEntity, TDto>(pagedRequest, _mapper);
        }

        public async Task<T> GetById(int id)
        {            
            return await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id==id);      
        }

        public async Task<T> TryGetById(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                throw new Exception();
            }
            return entity;
        }

        public async Task Add(T entity)
        {        
            await _dbContext.Set<T>().AddAsync(entity);
            //Audit();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {       
           _dbContext.Entry(entity).State = EntityState.Modified;
            // Audit();
           _dbContext.Update(entity);
            
        }

        public async Task<T> Remove(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new ValidateException($"Object {typeof(T)} with id:{id} not found");
            }
            _dbContext.Set<T>().Remove(entity);
            return entity;      
        }

        private IQueryable<TEntity> IncludeProperties<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

     

        public void Dispose() => _dbContext?.Dispose();
        
    }
}
