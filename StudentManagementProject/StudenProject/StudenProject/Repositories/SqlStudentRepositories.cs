using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudenProject.DataModels;
using StudenProject.DomainModels;
using System.Data;

namespace StudenProject.Repositories
{
    public class SqlStudentRepositories : IStudentRepositories
    {
        private readonly StudentAdminContext context;
        public SqlStudentRepositories(StudentAdminContext context)
        {
            this.context = context;
        }

        public async Task<Student> AddStudent(Student request)
        {
            // Student nesnesini oluştur
            var student = new Student
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                DateofBirth = request.DateofBirth,
                Email = request.Email,
                Phone = request.Phone,
                Gender = request.Gender,
                Address = new Address
                {
                    PhysicalAddress = request.Address.PhysicalAddress,
                    PostalAddress = request.Address.PostalAddress
                }
            };

            // Veritabanına ekle
            var addedStudent = await context.Student.AddAsync(student);
            await context.SaveChangesAsync();

            return addedStudent.Entity;
        }



        public async Task<Student> DeleteStudent(int studentId)
        {
            var existingStudent = await GetStudentAsync(studentId);
            if (existingStudent != null)
            {
                context.Student.Remove(existingStudent);
                await context.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        public async Task<bool> Exeist(int studentId)
        {
            return await context.Student.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student> GetStudentAsync(int studentId)
        {
            return await context.Student.Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> UpdateStudent(int studentId, Student request)
        {
            var existingStudent = await GetStudentAsync(studentId);
            if (existingStudent != null)
            {
                // İstek nesnesini doğrula
                if (request != null)
                {
                    // Adres null değilse güncelle
                    if (existingStudent.Address != null)
                    {
                        existingStudent.Firstname = request.Firstname;
                        existingStudent.Lastname = request.Lastname;
                        existingStudent.DateofBirth = request.DateofBirth;
                        existingStudent.Email = request.Email;
                        existingStudent.Phone = request.Phone;
                        existingStudent.Gender = request.Gender;

                        // Adres bilgilerini güncelle
                        existingStudent.Address.PhysicalAddress = request.Address?.PhysicalAddress;
                        existingStudent.Address.PostalAddress = request.Address?.PostalAddress;
                    }
                    else
                    {
                        // Adres null ise, yeni bir adres oluştur
                        existingStudent.Address = new Address
                        {
                            PhysicalAddress = request.Address?.PhysicalAddress,
                            PostalAddress = request.Address?.PostalAddress
                        };
                    }

                    // Değişiklikleri kaydet
                    await context.SaveChangesAsync();
                }

                // Güncellenmiş öğrenciyi döndür
                return existingStudent;
            }
            return null;
        }

        public async Task<StudentCourse> AddStudentCourse(StudentCourse studentStudentCourseEntity)
        {
            var addedEntity = await context.StudentCourse.AddAsync(studentStudentCourseEntity);
            await context.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await context.Course.Include(nameof(Period)).Include(nameof(Teacher)).ToListAsync();
        }

        public async Task<List<StudentCourse>> GetStudentsCourseAsync()
        {
            return await context.StudentCourse.Include(nameof(Student)).Include(nameof(Course)).ToListAsync();
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await context.User.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await context.Course
                .Include(c => c.Period) // Period ilişkisini yükle
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task AddInstallment(Installment installment)
        {
            context.Installment.Add(installment);
            await context.SaveChangesAsync();
        }

        public async Task<List<Installment>> GetInstallmentsAsync()
        {
            return await context.Installment.Include(nameof(Period)).Include(nameof(Student)).ToListAsync();
        }

        public async Task<Installment> UpdateInstallment(int installmentId, Installment updatedInstallment)
        {
            var existingInstallment = await context.Installment.FindAsync(installmentId);

            if (existingInstallment == null)
            {
                return null; // Belirtilen Id'ye sahip taksit bulunamadı.
            }

            existingInstallment.InstallmentC = updatedInstallment.InstallmentC;
            existingInstallment.Installmentt = updatedInstallment.Installmentt;
            existingInstallment.PaymentStatus = updatedInstallment.PaymentStatus;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // Eş zamanlı güncelleme hatası durumunda
            {
                // İlgili hata işlenir
                throw; // Gerekirse uygun bir şekilde yeniden yönlendirilir.
            }

            return existingInstallment;
        }

        public async Task<Installment> GetInstallmentById(int installmentId)
        {
            return await context.Installment
         .Include(i => i.Period) // Period özelliğini yükle
         .FirstOrDefaultAsync(i => i.Id == installmentId);
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            return await context.Student.FindAsync(studentId);
        }

        public async Task AddPaymentHistory(PaymentHistory paymentHistory)
        {
            try
            {
                context.PaymentHistory.Add(paymentHistory);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapılabilir
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                throw; // Hatanın dışarıya fırlatılması
            }
        }

        public async Task UpdateInstallmentPaymentStatus(int installmentId, string paymentStatus)
        {
            var installment = await context.Installment.FindAsync(installmentId);
            if (installment != null)
            {
                installment.PaymentStatus = paymentStatus;
                await context.SaveChangesAsync();
            }
        }

        public async Task AddDiscount(Discount discount)
        {
            context.Discount.Add(discount);
            await context.SaveChangesAsync();
        }

        public async Task<List<Discount>> GetDiscountsAsync()
        {
            return await context.Discount.Include(nameof(Student)).ToListAsync();
        }

        public async Task<bool> AllInstallmentsPaid(int studentId)
        {
            var installments = await context.Installment.Where(i => i.StudentId == studentId).ToListAsync();
            return installments.All(i => i.PaymentStatus == "A");

        }

        public async Task<decimal> GetTotalPaymentAmount(int studentId)
        {
            var installments = await context.Installment.Where(i => i.StudentId == studentId).ToListAsync();
            return installments.Sum(i => Convert.ToDecimal(i.Period.PaymentAmount));
        }

        public async Task<int> GetInstallmentCount(int studentId)
        {
            return await context.Installment.CountAsync(i => i.StudentId == studentId);
        }

        public async Task UpdateDiscountedPaymentAmount(int installmentId, decimal discountedPaymentAmount)
        {
            // Burada veritabanı işlemleri yapılacak
            // Örnek olarak Entity Framework kullanıyorsanız:
            var installment = await context.Installment.FindAsync(installmentId);
            if (installment != null)
            {
                // İndirimli ödeme miktarını hesapla
                var originalPaymentAmount = Convert.ToDecimal(installment.Period.PaymentAmount);
                var discountedAmount = originalPaymentAmount - discountedPaymentAmount;

                // DiscountedPaymentAmount yerine PaymentAmount'ı güncelle
                installment.Period.PaymentAmount = discountedAmount;
                await context.SaveChangesAsync();
            }
        }

        public async Task<Installment> AddInstallmentAsync(Installment installment)
        {
            await context.Installment.AddAsync(installment);
            await context.SaveChangesAsync();
            return installment;
        }

        public async Task<Period> GetPeriodById(int periodId)
        {
            return await context.Period.FirstOrDefaultAsync(p => p.Id == periodId);
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            context.Payments.Add(payment);
            await context.SaveChangesAsync();
            return payment;
        }
        public async Task<StudentCourse> AddStudentCourses(StudentCourse studentCourse)
        {
            var addedEntity = await context.StudentCourse.AddAsync(studentCourse);
            await context.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task Update(Payment payment)
        {
            context.Payments.Update(payment);
            await context.SaveChangesAsync();
        }

        public async Task<Payment> GetById(int id)
        {
            return await context.Payments.FindAsync(id);
        }

        public async void Update(Task<Payment> payment)
        {
            await Task.CompletedTask;
        }

        public async Task<Period> GetSelectedPeriod()
        {
            // Burada seçilen dönemi döndürmek için gerekli kodu yazın
            // Örnek olarak:
            return await context.Period.FirstOrDefaultAsync(p => p.Name == "Seçilen Dönem");
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync(); // SaveChangesAsync metodu SqlStudentRepository sınıfına eklendi
        }

        public async Task<Payment> GetPaymentById(int paymentId)
        {
            return await context.Payments.FindAsync(paymentId);
        }

        public async Task UpdatePayment(int paymentId, Payment payment)
        {
            var existingPayment = await context.Payments.FindAsync(paymentId);
            if (existingPayment != null)
            {
                existingPayment.PaymentType = payment.PaymentType;
                existingPayment.InstallmentCount = payment.InstallmentCount;

                context.Payments.Update(existingPayment);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            return await context.Payments.ToListAsync();
        }

        public async Task<List<PaymentHistory>> GetPaymentsHistoryAsync()
        {
            return await context.PaymentHistory.ToListAsync();
        }

        public async Task<List<Teacher>> GetTeacherssAsync()
        {
              return await context.Teacher.ToListAsync();
        }

        public async Task<Payment> GetPaymentAsync(int paymentId)
        {
            return await context.Payments.FirstOrDefaultAsync(x => x.Id == paymentId);
        }

        public  async Task<Student> GetStudentByNameAsync(string studentName)
        {
            return await context.Student.FirstOrDefaultAsync(s => s.Firstname == studentName);
        }

        public async Task AddStudentAsync(Student student)
        {
            await context.Student.AddAsync(student);
            await context.SaveChangesAsync();
        }

        public async Task<Course> GetCourseByNameAsync(string courseName)
        {
            return await context.Course.FirstOrDefaultAsync(c => c.Name == courseName);
        }

       

    }
}



