using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netcore.Sample.Web.Api.Configurations;
using Netcore.Sample.Web.Api.Models.Entities;

namespace Netcore.Sample.Web.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentContext _context;

        public StudentService(StudentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task CreateAsync(Student student)
        {
            _context.Students.Add(student);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
