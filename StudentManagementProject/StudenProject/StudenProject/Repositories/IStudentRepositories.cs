using StudenProject.DataModels;
using StudenProject.DomainModels;
using System.Threading.Tasks;

namespace StudenProject.Repositories
{
    public interface IStudentRepositories
    {
        Task<List<Student>> GetStudentsAsync();//Bütün öğrencileri çekme 
        Task<List<Teacher>> GetTeacherssAsync();//Bütün öğrencileri çekme 
        Task<List<Installment>> GetInstallmentsAsync();//Bütün öğrencileri çekme 
        Task<Student> GetStudentAsync(int studentId);//Tekbir öğrenciyi çekme 
        Task<bool> Exeist(int studentId);//Tekbir öğrenciyi çekme 
        Task<Student> UpdateStudent(int studentId, Student student);
        Task<Student> DeleteStudent(int studentId);
        Task<Student> AddStudent(Student student);  
      Task <StudentCourse> AddStudentCourse(StudentCourse studentStudentCourseEntity);
        Task<List<Course>> GetCoursesAsync();
        Task<List<StudentCourse>> GetStudentsCourseAsync();//Bütün öğrencileri çekme 
        Task<User> GetUserByUserName(string username);//Auth taskı 
        Task<Course> GetCourseById(int courseId);
        Task AddInstallment(Installment installment);
        Task<Installment> UpdateInstallment(int installmentId, Installment installment);
        Task<Installment> GetInstallmentById(int installmentId);
        Task<Student> GetStudentById(int studentId);
        Task AddPaymentHistory(PaymentHistory paymentHistory);
        Task UpdateInstallmentPaymentStatus(int installmentId, string paymentStatus);
        Task AddDiscount(Discount discount);
        Task<List<Discount>> GetDiscountsAsync();
        Task<bool> AllInstallmentsPaid(int studentId);
        Task<decimal> GetTotalPaymentAmount(int studentId);
        Task<int> GetInstallmentCount(int studentId);
        Task UpdateDiscountedPaymentAmount(int installmentId, decimal discountedPaymentAmount);
        Task<Installment> AddInstallmentAsync(Installment installment);
        Task<Period> GetPeriodById(int periodId);
        Task<Payment> AddPayment(Payment payment);
        Task<StudentCourse> AddStudentCourses(StudentCourse studentCourse);
        Task<Payment> GetById(int id);
        Task Update(Payment payment);
        void Update(Task<Payment> payment);
        Task<Period> GetSelectedPeriod();
        Task SaveChangesAsync();
        Task<Payment> GetPaymentById(int paymentId);
        Task UpdatePayment(int paymentId, Payment payment);
        Task<List<Payment>> GetPaymentsAsync();
        Task<Payment>GetPaymentAsync(int paymentId);
        Task<List<PaymentHistory>> GetPaymentsHistoryAsync();
        Task<Student> GetStudentByNameAsync(string studentName);
        Task AddStudentAsync(Student student);
        Task<Course> GetCourseByNameAsync(string courseName);



    }
}
