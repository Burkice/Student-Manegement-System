using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudenProject.Repositories;
using StudenProject.DomainModels;
using StudenProject.DataModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace StudenProject.Controllers
{
    [ApiController]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IStudentRepositories studentRepositories;
        private readonly IMapper mapper;

        public PaymentController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this.studentRepositories = studentRepositories;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllPaymentsAsync()
        {
            var payment = await studentRepositories.GetPaymentsAsync(); // Await kullanarak bütün öğrencileri çekme kodu
            return Ok(mapper.Map<List<Payments>>(payment));
        }

        [HttpPut]
        [Route("[controller]/{paymentId:int}"), ActionName("PaymentUpdateAsync")]

        public async Task<IActionResult> UpdatePaymentAsync(int paymentId, [FromBody] PaymentUpdateRequest request)
        {
            var existingPayment = await studentRepositories.GetPaymentById(paymentId);
            if (existingPayment == null)
            {
                return NotFound(); // Belirtilen Id'ye sahip ödeme bulunamadı
            }

            existingPayment.PaymentType = request.PaymentType;
            existingPayment.InstallmentCount = request.InstallmentCount;

            await studentRepositories.UpdatePayment(paymentId, existingPayment);

            if (request.PaymentType == "Peşin" && request.InstallmentCount == 0)
            {
                // Peşin ödeme tipi ve taksit sayısı 0 ise %10 indirim uygula
                var totalCourseFee = existingPayment.TotalCourseFee;
                var discountAmount = totalCourseFee * 0.1m;
                var discountedPaymentAmount = totalCourseFee - discountAmount;

                // Indirim bilgisini Discounts tablosuna ekleyin
                var discount = new Discount
                {
                    StudentId = existingPayment.StudentId,
                    Amount = discountAmount,
                    Rate = 10 // %10 indirim
                };

                await studentRepositories.AddDiscount(discount);

                // Indirim uygulanmış tutarı PaymentHistory tablosuna ekleyin
                await studentRepositories.AddPaymentHistory(new PaymentHistory
                {
                    PeriodId = existingPayment.CourseId,
                    InstallmentId = null,
                    PaymentAmount = discountedPaymentAmount,
                    RecordStatus = "A",
                    PaymentType = "Peşin"
                });
            }

            if (request.PaymentType == "Taksit" && request.InstallmentCount > 0)
            {
                // Taksit ödeme tipi ve taksit sayısı 0'dan büyükse Installment tablosuna yeni kayıtlar ekle
                for (int i = 1; i <= request.InstallmentCount; i++)
                {
                    var newInstallment = new Installment
                    {
                        StudentId = existingPayment.StudentId,
                        PeriodId = existingPayment.CourseId,
                        InstallmentC = i, // Taksit numarasını ayarlayın
                        PaymentStatus = "P", // Ödeme durumunu belirtin
                        Installmentt = "Taksit" // Ödeme tipini belirtin
                    };

                    await studentRepositories.AddInstallmentAsync(newInstallment);
                }

                // Taksit ödeme tipi ve taksit sayısı 0'dan büyükse PaymentHistory tablosuna ek veri ekle
                await studentRepositories.AddPaymentHistory(new PaymentHistory
                {
                    PeriodId = existingPayment.CourseId,
                    InstallmentId = null,
                    PaymentAmount = existingPayment.TotalCourseFee,
                    RecordStatus = "P",
                    PaymentType = "Taksit"
                });
            }

            return Ok(existingPayment);
        }

        [HttpGet]
        [Route("[controller]/{paymentId:int}")]
        public async Task<IActionResult> GetAllPaymentAsync([FromRoute] int paymentId)
        {
            var paymen = await studentRepositories.GetPaymentAsync(paymentId);
           if(paymen == null)
            {
                return NotFound();
            } 
           
            return Ok(mapper.Map<Payments>(paymen));
        }


    }
}
