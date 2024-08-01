using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.DTO;
using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {



        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        //private readonly ICollegeRepository<Student> _studentRepository;  //since IStudentRepository is inheriting from ICollegeRepository, hence commeting this line
        private readonly IStudentRepository _studentRepository;


        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentyRepository)
        {
            _logger = logger;
            _studentRepository = studentyRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudents()
        {

            _logger.LogInformation("Get All student getting executed");
            //var result = await _dbContext.Students.Select(x => new StudentDto
            //{

            //    Address = x.Address,
            //    Email = x.Email,
            //    Id = x.Id,
            //    StudentName = x.StudentName,
            //    DOB=x.DOB

            //}).ToListAsync();


            var result = await _studentRepository.GetAllAsync();
            var studentDtoData=_mapper.Map<List<StudentDto>>(result);


            return Ok(studentDtoData);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> GetStudentsById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
               

            var result = await _studentRepository.GetAsync(student=>student.Id==id);



            if (result == null)
            {
                _logger.LogError("Student ID not Found");
                return NotFound($"student with the id {id} not found");
            }
                


            //var student = new StudentDto
            //{
            //    Id = result.Id,
            //    StudentName = result.StudentName,
            //    Email = result.Email,
            //    Address = result.Address
                
            //};

            var student=_mapper.Map<StudentDto>(result);

            return student;
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<Student>> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var result = await _studentRepository.GetAsync(student=> student.StudentName==name);

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

        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] StudentDto model)
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

            //var newStudent = new Student
            //{
                
            //    Address = model.Address,
            //    Email = model.Email,
            //    StudentName = model.StudentName,
            //    DOB = model.DOB
            //};

            var newStudent=_mapper.Map<Student>(model);

            
            await _studentRepository.CreateAsync(newStudent);

            model.Id=newStudent.Id;

            //status code will be 201
            //return CreatedAtRoute("GetStudentById", new { model.Id },model);

             return Ok(newStudent);
        }


        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> UpdateStudentAsync([FromBody] StudentDto model) 
        {  
            if (model == null || model.Id<0) 
                return BadRequest();

            var existingStudent = await _studentRepository.GetAsync(student=>student.Id == model.Id, true);

            if (existingStudent == null)
                   return NotFound();

            //existingStudent.StudentName = model.StudentName;
            //existingStudent.DOB = model.DOB;
            //existingStudent.Email = model.Email;

            var newRecordForSameID=_mapper.Map<Student>(model);


            _studentRepository.UpdateAsync(newRecordForSameID);

            return NoContent();
        
        }


        //[HttpPatch]
        //[Route("{id:int}/UpdatePartial")]
        //api/student/1/updatepartial
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDto> patchDocument)
        //{
        //    if (patchDocument == null || id <= 0)
        //        BadRequest();

        //    var existingStudent = _studentyRepository.GetByIdAsync(id);

        //    if (existingStudent == null)
        //        return NotFound();

        //    var studentDTO = new StudentDto
        //    {
        //        Id = existingStudent.Id,
        //        StudentName = existingStudent,
        //        Email = existingStudent.Email,
        //        Address = existingStudent.Address
        //    };

        //    //patchDocument.ApplyTo(studentDTO, ModelState);

        //    patchDocument.ApplyTo(studentDTO);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    existingStudent.StudentName = studentDTO.StudentName;
        //    existingStudent.Email = studentDTO.Email;
        //    existingStudent.Address = studentDTO.Address;

        //    //204 - NoContent
        //    return NoContent();
        //}


        [HttpDelete("Delete/{id}", Name = "DeleteStudentById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudentAsync(int id)
        {
            //BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = await _studentRepository.GetAsync(student => student.Id == id );
            //NotFound - 404 - NotFound - Client error
            if (student == null)
                return NotFound($"The student with id {id} not found");

            await _studentRepository.DeleteAsync(student);

            //OK - 200 - Success
            return Ok(true);
        }

    }
}
