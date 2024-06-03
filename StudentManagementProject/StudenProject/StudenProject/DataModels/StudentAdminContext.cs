using Microsoft.EntityFrameworkCore;

namespace StudenProject.DataModels
{
    public class StudentAdminContext:DbContext
    {
        public StudentAdminContext(DbContextOptions<StudentAdminContext>options):base(options) 
        {
        }

        public  DbSet<Student> Student { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Installment> Installment { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
        public DbSet<Period> Period { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Teacher> Teacher{ get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<Payment> Payments { get; set; }

    }
}
