using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repositories.Abstracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        public IQueryable<T> GetQuery();
        void DeleteRange(IEnumerable<T> entities);


    }
}
