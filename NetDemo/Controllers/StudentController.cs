using Microsoft.AspNetCore.Mvc;
using NetDemo.Models;
using NetDemo.Repository;

namespace NetDemo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("all", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<StudentModel>> GetStudents()
        {
            _logger.LogInformation("Get students method started.");
            return Ok(CollegeRepository.Students);
        }

        [HttpGet]
        [Route("{id}", Name = "GetStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();
            var student = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"Id {id} not found");
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };
            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
                return BadRequest();
            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            StudentModel student = new StudentModel()
            {
                Id = newId,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address
            };
            CollegeRepository.Students.Add(student);
            model.Id = newId;
            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public bool DeleteStudent(int id)
        {
            var student = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();
            CollegeRepository.Students.Remove(student);
            return true;
        }

    }
}
