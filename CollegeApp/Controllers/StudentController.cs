using CollegeApp.DTO;
using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<StudentDto> GetAllStudents()
        {


            var result = CollegeRepository.Students.Select(x => new StudentDto
            {

                Address = x.Address,
                Email = x.Email,
                Id = x.Id,
                StudentName = x.StudentName


            });


            return result;
        }

        [HttpGet("{id:int}")]
        [Route("GetStudentsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDto> GetStudentsById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = CollegeRepository.Students.FirstOrDefault(x => x.Id == id);



            if (result == null)
                return NotFound($"student with the id {id} not found");


            var student = new StudentDto
            {
                Id = result.Id,
                StudentName = result.StudentName,
                Email = result.Email,
                Address = result.Address
            };

            return student;
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var result = CollegeRepository.Students.FirstOrDefault(x => x.StudentName == name);

            if (result == null)
                return NotFound($"student with the id {name} not found");

            return result;
        }

        [HttpPost]
        [Route("CreateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDto> CreateStudent([FromBody] StudentDto model)
        {
            if (model == null)
                return BadRequest();

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;

            var newStudent = new Student
            {
                Id = newId,
                Address = model.Address,
                Email = model.Email,
                StudentName = model.StudentName
            };

            model.Id = newStudent.Id;

            CollegeRepository.Students.Add(newStudent);

            return Ok(newStudent);
        }

    }
}
