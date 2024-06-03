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
    public class TeacherController : Controller
    {
        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;

        public TeacherController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllTeachersAsync()
        {
            var teacher = await studentRepositories.GetTeacherssAsync(); // Await kullanarak bütün öğrencileri çekme kodu
            return Ok(mapper.Map<List<Teachers>>(teacher));
        }
    }
}
