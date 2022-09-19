using System.Threading.Tasks;

namespace Educative.Infrastructure.Interface
{
    public interface IUnitOfWork 
    {
        IAddressRepository AddressRepository { get; }

        ICourseRepository CourseRepository { get; }

        IStudentRepository StudentRepository { get; }

        Task<bool> Complete();

        bool HasChanges();
    }
}