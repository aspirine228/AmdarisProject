
using GameTracker.Domain.Entities;
using GameTracker.Rep.Interfaces;
using GameTracker.Rep;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System.Linq.Expressions;

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
       
        
        public async Task<T> GetById(int id)
        {            
            return await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id==id);      //entity;
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
          // _dbContext.Update(entity).Entity;
            
        }
        public async Task<T> Remove(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Object not found");
            }

            _dbContext.Set<T>().Remove(entity);
            return entity;
            
        }
     
     

       public void Dispose() => _dbContext?.Dispose();
        
    }
}
