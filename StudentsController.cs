using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Netcore.Sample.Web.Api.Models;
using Netcore.Sample.Web.Api.Models.Entities;
using Netcore.Sample.Web.Api.Services;

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
        public async Task<ActionResult<IEnumerable<StudentDTO>>> Get()
        {
            var students = await _studentService.GetAsync();
            var studentsDTO = students.Select(student => StudentDTO.fromEntity(student));
            return Ok(studentsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> Get(int id)
        {
            var student = await _studentService.GetAsync(id);

            if (student == null)
                return NotFound();

            return StudentDTO.fromEntity(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Post([FromBody] StudentDTO studentDTO)
        {
            var student = new Student();
            student.fromDTO(studentDTO);
            await _studentService.CreateAsync(student);

            studentDTO = StudentDTO.fromEntity(student);
            return CreatedAtAction(nameof(Get), new { id = studentDTO.Id }, studentDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
                return BadRequest();

            var student = await _studentService.GetAsync(id);
            if (student == null)
                return NotFound();

            student.fromDTO(studentDTO);

            await _studentService.UpdateAsync(student);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var student = await _studentService.GetAsync(id);

            if (student == null)
                return NotFound();

            await _studentService.DeleteAsync(student);

            return NoContent();
        }
    }
}