using System.Collections.Generic;
using System.Threading.Tasks;
using Educative.Infrastructure.Context;
using Educative.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EducativeContext _context;

        public GenericRepository(EducativeContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        
        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Detached;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
           
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() == 1;


        }

        

        


        
    }
}