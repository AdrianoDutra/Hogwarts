using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hogwarts.Domain.Entities;

namespace Hogwarts.Domain.Interfaces.Repository
{

    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistAsync(Guid id);
        Task<T> SelectAsync(Guid id);
    }
}

