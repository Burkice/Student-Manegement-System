using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudenProject.DataModels;
using StudenProject.DomainModels;
using StudenProject.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
[Authorize]
public class InstallmentController : ControllerBase
{
    private readonly IStudentRepositories _studentRepositories;
    private readonly IMapper _mapper;

    public InstallmentController(IStudentRepositories studentRepositories, IMapper mapper)
    {
        _studentRepositories = studentRepositories;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Installment>>> GetInstallmentsAsync()
    {
        var installments = await _studentRepositories.GetInstallmentsAsync();
        return Ok(_mapper.Map<List<Installment>>(installments));
    }

    [HttpPost] // Ekleme işlemi
    [Route("pay")] // Belirli bir rotaya bağlama
    public async Task<IActionResult> PayInstallmentAsync([FromBody] PayInstallmentModel model)
    {
        var installment = await _studentRepositories.GetInstallmentById(model.InstallmentId);
        if (installment == null)
        {
            return NotFound(); // Belirtilen ID'ye sahip taksit bulunamadı
        }

        if (installment.PaymentStatus == "A")
        {
            return BadRequest("This installment has already been paid."); // Bu taksit zaten ödendi
        }

        if (model.AmountPaid <= 0)
        {
            return BadRequest("Invalid payment amount."); // Geçersiz ödeme miktarı
        }

        if (model.AmountPaid > Convert.ToDecimal(installment.Period.PaymentAmount))
        {
            return BadRequest("Payment amount exceeds the remaining installment amount."); // Ödeme miktarı kalan taksit tutarını aşıyor
        }

        // Ödeme geçmişi tablosuna tek bir kayıt ekle
        await _studentRepositories.AddPaymentHistory(new PaymentHistory
        {
            PeriodId = installment.PeriodId,
            InstallmentId = model.InstallmentId,
            PaymentAmount = model.AmountPaid, // Ödenen miktarı ekleyin
            RecordStatus = "A", // Ödeme durumu "A" olarak işaretlenir 
            PaymentType = "Taksit", // Ödeme tipi taksit
        });

        // Installment tablosunda paymentStatus'u güncelle
        installment.PaymentStatus = "A"; // Ödeme yapıldı
        await _studentRepositories.UpdateInstallmentPaymentStatus(model.InstallmentId, "A");

        return Ok(installment);
    }
}

// PayInstallmentModel Sınıfı
public class PayInstallmentModel
{
    public int InstallmentId { get; set; }
    public decimal AmountPaid { get; set; }
}
