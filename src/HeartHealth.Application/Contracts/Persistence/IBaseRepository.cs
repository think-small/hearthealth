using HeartHealth.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeartHealth.Application.Contracts.Persistence
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
    }
}
