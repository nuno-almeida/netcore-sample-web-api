using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netcore.Sample.Web.Api.Configurations;
using Netcore.Sample.Web.Api.Models;
using Netcore.Sample.Web.Api.Models.DTOs;
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

        public async Task<PagedList<Student>> GetAsync(GetStudentsQueryDTO getStudentsQueryDTO)
        {
            var queryStudents = getStudentsQueryDTO.GetQuery(_context.Students);
            var pageIndex = getStudentsQueryDTO.Page;
            var pageSize = getStudentsQueryDTO.PageSize;

            return await PagedList<Student>.CreateAsync(queryStudents, pageIndex, pageSize);
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
