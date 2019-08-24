using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearch.Repositories
{
    public interface IDataRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> SaveAsync(T entity);
    }
}
