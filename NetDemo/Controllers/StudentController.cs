using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetDemo.Data;
using NetDemo.Data.Repository;
using NetDemo.Models;

namespace NetDemo.Controller
{
    [Route("student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Student> _studentRepository;
        public StudentController(ILogger<StudentController> logger, IMapper mapper, ICollegeRepository<Student> studentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Route("", Name = "GetAllStudents")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
        {
            _logger.LogInformation("Get students method started.");
            var students = await _studentRepository.GetAllAsync();
            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);
            return Ok(studentsDTO);
        }

        [HttpGet]
        [Route("{id}", Name = "GetStudentById")]
        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = await _studentRepository.GetByIdAsync(student => student.Id == id);
            if (student == null)
                return NotFound($"Id {id} not found");
            var studentDTO = _mapper.Map<StudentDTO>(student);
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentDTO dto)
        {
            if (dto == null)
                return BadRequest();

            Student student = _mapper.Map<Student>(dto);

            var newStudent = await _studentRepository.CreateAsync(student);

            var studentDTO = _mapper.Map<StudentDTO>(newStudent);
            return CreatedAtRoute(student, studentDTO);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<StudentDTO>> UpdateStudent([FromBody] StudentDTO dto, int id)
        {
            if (dto == null || id < 0)
                return BadRequest();

            var existingStudent = await _studentRepository.GetByIdAsync(student => student.Id == id, true);

            if (existingStudent == null)
                return NotFound($"Id {id} not found");
            var newStudent = _mapper.Map<Student>(dto);
            await _studentRepository.UpdateAsync(newStudent);
            return Ok(newStudent);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<StudentDTO>> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();
            var student = await _studentRepository.GetByIdAsync(student => student.Id == id);
            if (student == null)
                return NotFound($"The student not found with id {id}");

            var delStudent = _mapper.Map<Student>(student);
            await _studentRepository.DeleteAsync(delStudent);
            return Ok(delStudent);
        }

    }
}
