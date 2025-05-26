using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetDemo.Data;
using NetDemo.Models;

namespace NetDemo.Controller
{
    [Route("student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDBContext _dbContext;
        private readonly IMapper _mapper;
        public StudentController(ILogger<StudentController> logger, CollegeDBContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("", Name = "GetAllStudents")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
        {
            _logger.LogInformation("Get students method started.");
            var students = await _dbContext.Students.ToListAsync();
            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);
            return Ok(studentsDTO);
        }

        [HttpGet]
        [Route("{id}", Name = "GetStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();
            var student = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"Id {id} not found");
            var studentDTO = _mapper.Map<StudentDTO>(student);
            return Ok(studentDTO);
        }

        [HttpPost]
        //public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        //{
        //    if (model == null)
        //        return BadRequest();

        //    // Map StudentDTO to Student
        //    Student student = new Student()
        //    {
        //        Id = _dbContext.Students.Any() ? _dbContext.Students.Max(s => s.Id) + 1 : 1, // Generate new ID
        //        StudentName = model.StudentName,
        //        Email = model.Email,
        //        Address = model.Address,
        //        DOB = DateTime.Now // Assuming DOB is required, set a default value
        //    };

        //    _dbContext.Students.Add(student);
        //    _dbContext.SaveChanges();

        //    // Map back to StudentDTO to return
        //    model.Id = student.Id;
        //    return Ok(model);
        //}

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model, int id)
        {
            if (model == null || id < 0)
                return BadRequest();

            var existingStudent = _dbContext.Students.AsNoTracking().Where(s => s.Id == id).FirstOrDefault();
            if (existingStudent == null)
                return NotFound($"Id {id} not found");
            var newStudent = new Student()
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address,
                DOB = existingStudent.DOB
            };
            _dbContext.Students.Update(newStudent);
            _dbContext.SaveChanges();
            return Ok(newStudent);
        }

        [HttpDelete("{id:int}")]
        public bool DeleteStudent(int id)
        {
            var student = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();
            _dbContext.Students.Remove(student);
            return true;
        }

    }
}
