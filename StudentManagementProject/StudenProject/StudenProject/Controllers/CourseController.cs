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

    public class CourseController : Controller
    {
        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;
        public CourseController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetCoursesAsync()
        {
            var course = await studentRepositories.GetCoursesAsync(); // Await kullanarak bütün kursları çekme kodu
            return Ok(mapper.Map<List<Courses>>(course));
        }
       

    }
}
