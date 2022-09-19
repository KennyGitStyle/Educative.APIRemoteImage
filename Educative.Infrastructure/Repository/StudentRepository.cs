using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educative.Core;
using Educative.Infrastructure.Context;
using Educative.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Repository
{
    public class
    StudentRepository
    : GenericRepository<Student>, IStudentRepository
    {
        private readonly EducativeContext _context;

        public StudentRepository(EducativeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> FilterAttendanceAsync(Expression<Func<Student, bool>> predicate)
        {
             return await _context.Students
                .Include(sa => sa.Address)
                .Include(sc => sc.StudentCourses)
                .ThenInclude(a => a.Course)
                .Where(predicate).OrderBy(x => x.Lastname).ToListAsync();   
        }
    }
}
