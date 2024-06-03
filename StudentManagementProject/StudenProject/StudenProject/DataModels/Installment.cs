using System.ComponentModel.DataAnnotations.Schema;

namespace StudenProject.DataModels
{
    public class Installment
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int InstallmentC { get; set; } //taksit sayısı
        public string? Installmentt { get; set; }//ödeme tipi
        public string? PaymentStatus { get; set; } 
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [ForeignKey("PeriodId")]
        public Period Period { get; set; }
    }
}


