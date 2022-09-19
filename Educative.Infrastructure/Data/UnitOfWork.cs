using System.Threading.Tasks;
using Educative.Infrastructure.Context;
using Educative.Infrastructure.Interface;
using Educative.Infrastructure.Repository;

namespace Educative.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EducativeContext _context;
        public UnitOfWork(EducativeContext context)
        {
            _context = context;
        }

        public IAddressRepository AddressRepository => new AddressRepository(_context);

        public ICourseRepository CourseRepository => new CourseRepository(_context);

        public IStudentRepository StudentRepository => new StudentRepository(_context);
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}