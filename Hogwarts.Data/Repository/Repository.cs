using Hogwarts.Data.Context;
using Hogwarts.Domain.Entities;
using Hogwarts.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Hogwarts.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _myContext;
        private DbSet<T> _dbSet;
        public Repository(MyContext myContext)
        {
            _myContext = myContext;
            _dbSet = _myContext.Set<T>();
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                _dbSet.Add(item);
                await _myContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return item;
        }
        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dbSet.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                {
                    return null;
                }
                _myContext.Entry(result).CurrentValues.SetValues(item);
                await _myContext.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dbSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    return false;
                }
                _dbSet.Remove(result);
                await _myContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dbSet.AnyAsync(p => p.Id.Equals(id));
        }
          
        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dbSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch 
            {
                throw;
            }
        }
    }
}
