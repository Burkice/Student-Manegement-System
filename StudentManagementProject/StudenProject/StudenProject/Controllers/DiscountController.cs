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
    public class DiscountController : Controller
    {
        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;

        public DiscountController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllDiscountAsync()
        {
            var discount = await studentRepositories.GetDiscountsAsync(); // Await kullanarak bütün taksitleri çekme kodu
            return Ok(mapper.Map<List<Discounts>>(discount));
        }
    }
}

