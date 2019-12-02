using System.Collections.Generic;
using System.Threading.Tasks;
using E_CommerceCore.Core.Domain.Entities.Interfaces;

namespace E_CommerceCore.Core.Domain.Repositories.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}