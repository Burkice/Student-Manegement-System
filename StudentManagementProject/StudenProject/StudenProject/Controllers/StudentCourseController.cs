using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudenProject.DataModels;
using StudenProject.DomainModels;
using StudenProject.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudenProject.Controllers
{
    [ApiController]
    [Authorize]
    public class StudentCourseController : Controller
    {
        private readonly IStudentRepositories _studentRepositories;
        private readonly IMapper _mapper;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public StudentCourseController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            _studentRepositories = studentRepositories;
            _mapper = mapper;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64 // İstenilen derinlik değeri
            };
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentCourseAsync()
        {
            var studentCourses = await _studentRepositories.GetStudentsCourseAsync();
            var studentCourseDTOs = _mapper.Map<List<StudentCourseDTO>>(studentCourses);
            return Ok(studentCourseDTOs);
        }

        [HttpPost]
        [Route("[controller]/AddStudentCourses")]
        public async Task<IActionResult> AddStudentCoursesAsync([FromBody] AddStudentCourseRequest request)
        {
            _jsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            _jsonSerializerOptions.MaxDepth = 64;

            // Öğrenciyi veritabanında bul veya oluştur
            var student = await _studentRepositories.GetStudentByNameAsync(request.StudentName);
            if (student == null)
            {
                student = new Student { Firstname = request.StudentName };
                await _studentRepositories.AddStudentAsync(student);
            }

            // Kursların toplam ücretini hesaplamak için ID'lerini topla
            List<int> courseIds = new List<int>();
            foreach (var courseName in request.CourseNames)
            {
                var course = await _studentRepositories.GetCourseByNameAsync(courseName);
                if (course == null)
                {
                    return NotFound($"Course with name {courseName} not found.");
                }
                courseIds.Add(course.Id);
            }

            decimal totalCourseFee = await CalculateTotalCourseFee(courseIds);

            // StudentCourse nesnelerini oluştur ve veritabanına ekle
            foreach (int courseId in courseIds)
            {
                var studentCourse = new StudentCourse
                {
                    StudentId = student.Id,
                    CourseId = courseId
                };
                await _studentRepositories.AddStudentCourse(studentCourse);
            }

            // Payment nesnesini oluştur
            Payment payment = new Payment
            {
                StudentId = student.Id,
                TotalCourseFee = totalCourseFee,
                CourseId = courseIds.FirstOrDefault() // Sadece bir kurs için ödeme yapılıyor varsayıldı, gereksinimlere göre değiştirilebilir
            };

            // Payment nesnesini veritabanına ekle
            Payment addedPayment = await _studentRepositories.AddPayment(payment);

            // addedPayment nesnesini JSON formatına dönüştür
            string json = JsonSerializer.Serialize(addedPayment, _jsonSerializerOptions);

            return Ok(json);
        }

        private async Task<decimal> CalculateTotalCourseFee(List<int> courseIds)
        {
            decimal totalFee = 0;

            foreach (int courseId in courseIds)
            {
                Course course = await _studentRepositories.GetCourseById(courseId);

                // Kursun dönem bilgisinin null olup olmadığını kontrol et
                if (course != null && course.Period != null)
                {
                    totalFee += course.Period.PaymentAmount; // Kursun ait olduğu dönemin ücretini toplam ücrete ekle
                }
                else
                {
                    // Kursun veya dönemin bilgisi null ise, bir hata iletisi gönder ve işlemi iptal et
                    throw new Exception($"Course with Id {courseId} does not have a valid period.");
                }
            }

            return totalFee;
        }
    }
}
