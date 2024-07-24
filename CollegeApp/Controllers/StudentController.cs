using CollegeApp.Data;
using CollegeApp.DTO;
using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {



        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDBContext _dbContext;

        public StudentController(ILogger<StudentController> logger, CollegeDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<StudentDto> GetAllStudents()
        {

            _logger.LogInformation("Get All student getting executed");
            var result = _dbContext.Students.Select(x => new StudentDto
            {

                Address = x.Address,
                Email = x.Email,
                Id = x.Id,
                StudentName = x.StudentName,
                DOB=x.DOB

            });


            return result;
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDto> GetStudentsById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
               

            var result = _dbContext.Students.FirstOrDefault(x => x.Id == id);



            if (result == null)
            {
                _logger.LogError("Student ID not Found");
                return NotFound($"student with the id {id} not found");
            }
                


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

            var result = _dbContext.Students.FirstOrDefault(x => x.StudentName == name);

            if (result == null)
                return NotFound($"student with the id {name} not found");

            return result;
        }

        [HttpPost]
        [Route("CreateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDto> CreateStudent([FromBody] StudentDto model)
        {
            if (model == null)
                return BadRequest();

            

            //Directly adding error message

            //if (model.AdmissionDate <DateTime.Now)
            //{
            //    ModelState.AddModelError("admission date Error", "admission date cannot be less than today's date");
            //    return BadRequest();
            //}


            //using custom attribute

            var newStudent = new Student
            {
                
                Address = model.Address,
                Email = model.Email,
                StudentName = model.StudentName,
                DOB = model.DOB
            };

            

            _dbContext.Students.Add(newStudent);
            _dbContext.SaveChanges();

            //status code will be 201
            //return CreatedAtRoute("GetStudentById", new { model.Id },model);

             return Ok(newStudent);
        }


        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult UpdateStudent([FromBody] StudentDto model) 
        {  
            if (model == null || model.Id<0) 
                return BadRequest();

            var existingStudent=  _dbContext.Students.FirstOrDefault(x => x.Id == model.Id);

            if (existingStudent == null)
                   return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.DOB = model.DOB;
            existingStudent.Email = model.Email;

            _dbContext.Students.Update(existingStudent);
            _dbContext.SaveChanges();

            return NoContent();
        
        }


        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //api/student/1/updatepartial
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDto> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var existingStudent = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var studentDTO = new StudentDto
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address
            };

            //patchDocument.ApplyTo(studentDTO, ModelState);

            patchDocument.ApplyTo(studentDTO);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;

            //204 - NoContent
            return NoContent();
        }


        [HttpDelete("Delete/{id}", Name = "DeleteStudentById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            //BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();
            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with id {id} not found");

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            //OK - 200 - Success
            return Ok(true);
        }

    }
}
