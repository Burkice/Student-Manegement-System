using StudenProject.DataModels;

namespace StudenProject.DomainModels
{
    public class Payments
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public decimal TotalCourseFee { get; set; } // Kursların toplam ücreti
        public string? PaymentType { get; set; } // Taksit veya Peşin
        public int? InstallmentCount { get; set; } // Taksit sayısı (sadece taksit ödemeleri için)
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
