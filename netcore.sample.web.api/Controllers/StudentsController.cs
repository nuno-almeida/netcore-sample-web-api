using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Netcore.Sample.Web.Api.Filters;
using Netcore.Sample.Web.Api.Models;
using Netcore.Sample.Web.Api.Models.DTOs;
using Netcore.Sample.Web.Api.Models.Entities;
using Netcore.Sample.Web.Api.Services;
using Netcore.Sample.Web.Api.Utils;

namespace Netcore.Sample.Web.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }
        
        [HttpGet]
        [AuditFilter(Operation = Constants.Audit.Operations.ReadAll, EntityName = Constants.Audit.Entities.Student)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> Get()
        {
            var students = await _studentService.GetAsync();
            var studentsDTO = students.Select(student => StudentDTO.fromEntity(student));
            return Ok(studentsDTO);
        }

        [HttpGet("{id}")]
        [AuditFilter(Operation = Constants.Audit.Operations.Read, EntityName = Constants.Audit.Entities.Student)]
        public async Task<ActionResult<StudentDTO>> Get(int id)
        {
            var student = await _studentService.GetAsync(id);

            if (student == null)
                return HttpResponseResult.NotFound($"Student {id} not found");

            return StudentDTO.fromEntity(student);
        }

        [HttpPost]
        [AuditFilter(Operation = Constants.Audit.Operations.Create, EntityName = Constants.Audit.Entities.Student)]
        public async Task<ActionResult<StudentDTO>> Post([FromBody] StudentDTO studentDTO)
        {
            var student = new Student();
            student.fromDTO(studentDTO);
            await _studentService.CreateAsync(student);

            studentDTO = StudentDTO.fromEntity(student);
            return CreatedAtAction(nameof(Get), new { id = studentDTO.Id }, studentDTO);
        }

        [HttpPut("{id}")]
        [AuditFilter(Operation = Constants.Audit.Operations.Update, EntityName = Constants.Audit.Entities.Student)]
        public async Task<IActionResult> Put(int id, [FromBody] StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
                return HttpResponseResult.BadRequest("Student path id and body id mismatch");

            var student = await _studentService.GetAsync(id);
            if (student == null)
                return HttpResponseResult.NotFound($"Student {id} not found");

            student.fromDTO(studentDTO);

            await _studentService.UpdateAsync(student);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuditFilter(Operation = Constants.Audit.Operations.Delete, EntityName = Constants.Audit.Entities.Student)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var student = await _studentService.GetAsync(id);

            if (student == null)
                return HttpResponseResult.NotFound($"Student {id} not found");

            await _studentService.DeleteAsync(student);

            return NoContent();
        }
    }
}