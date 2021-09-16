using GameTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameTracker.Rep.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<T> TryGetById(int id);
        List<T> GetAll();
        Task SaveChangesAsync();
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        Task Add(T user);
        Task Update(T user);
        Task<T> Remove(int id);

       // Dictionary<int, string> CreateDic();
    }
}
