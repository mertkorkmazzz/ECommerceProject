using ECommerce.Data.Context;
using ECommerce.Data.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repositories.Concretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbset;

        public GenericRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbset = _dbContext.Set<T>();
        }






        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
           return _dbset.AsQueryable();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
    }
}
