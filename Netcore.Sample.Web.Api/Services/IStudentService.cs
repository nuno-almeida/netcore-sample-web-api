using System.Threading.Tasks;
using Netcore.Sample.Web.Api.Models;
using Netcore.Sample.Web.Api.Models.DTOs;
using Netcore.Sample.Web.Api.Models.Entities;

namespace Netcore.Sample.Web.Api.Services
{
    public interface IStudentService
    {
        Task<PagedList<Student>> GetAsync(GetStudentsQueryDTO getStudentsQueryDTO);
        Task<Student> GetAsync(int id);
        Task CreateAsync(Student studentDTO);
        Task UpdateAsync(Student studentDTO);
        Task DeleteAsync(Student studentDTO);
    }
}
