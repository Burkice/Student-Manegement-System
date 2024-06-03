using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudenProject.DataModels;
using StudenProject.DomainModels;
using StudenProject.Repositories;

namespace StudenProject.Controllers
{
    [ApiController]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;
        public StudentController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentAsync()
        {
            var studens = await studentRepositories.GetStudentsAsync(); // Await kullanarak bütün öğrencileri çekme kodu
            return Ok(mapper.Map<List<Students>>(studens));
        }


        [HttpGet]
        [Route("[controller]/{studentId:int}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] int studentId)
        {
            var student = await studentRepositories.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Students>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:int}"), ActionName("UpdateStudentAsync")]   
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] int studentId, [FromBody] UpdateStudentRequest request)
        {
            if(await studentRepositories.Exeist(studentId))
            {
                var updatestudent= await studentRepositories.UpdateStudent(studentId, mapper.Map<Student>(request));
                if(updatestudent == null)
                {
                    return Ok(mapper.Map<Students>(updatestudent));
                }
            }
            return NotFound();  
          
        }

        [HttpDelete] //silme işlemi
        [Route("[controller]/{studentId:int}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] int studentId)
        {
            if (await studentRepositories.Exeist(studentId))
            {
                var student = await studentRepositories.DeleteStudent(studentId);
                if (student != null)
                {
                    return Ok(mapper.Map<Students>(student));
                }
            }
            return NotFound();
        }

        [HttpPost] //ekleme işlemi
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await studentRepositories.AddStudent(mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id }, mapper.Map<Student>(student));
        }
    }
}
