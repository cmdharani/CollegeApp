using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController:ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<Student> GetAllStudents()
        {
            return CollegeRepository.Students;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentsById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = CollegeRepository.Students.FirstOrDefault(x => x.Id == id);

            if(result == null)
                return NotFound($"student with the id {id} not found");

            return result;
        }

        [HttpGet("{name:alpha}",Name= "GetStudentByName")]
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
    }
}
