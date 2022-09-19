using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Educative.Core;

namespace Educative.Infrastructure.Interface
{
    public interface IStudentRepository : IGenericRepository<Student> {

        Task<IEnumerable<Student>> FilterAttendanceAsync(Expression<Func<Student, bool>> predicate);

    }
}
