using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudenProject.DomainModels;
using StudenProject.Repositories;

namespace StudenProject.Controllers
{
    [ApiController]
    [Authorize]
    public class PaymentHistoryController : Controller
    {
        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;

        public PaymentHistoryController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllPaymentsHistoryAsync()
        {
            var paymenthistory = await studentRepositories.GetPaymentsHistoryAsync(); // Await kullanarak bütün öğrencileri çekme kodu
            return Ok(mapper.Map<List<PaymentHistorys>>(paymenthistory));
        }
    }
}
