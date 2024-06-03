using System.ComponentModel.DataAnnotations.Schema;

namespace StudenProject.DataModels
{
    public class PaymentHistory
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int? InstallmentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string? RecordStatus { get; set; }
        public string? PaymentType { get; set; }

        

        public Period Period { get; set; }
        public Installment Installment { get; set; }
    }
}
