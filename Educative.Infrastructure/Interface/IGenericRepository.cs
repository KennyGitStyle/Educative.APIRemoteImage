using System.Collections.Generic;
using System.Threading.Tasks;

namespace Educative.Infrastructure.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);

        Task<IEnumerable<T>> GetAllAsync();
       
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool?> DeleteAsync(string id);
    }
}
