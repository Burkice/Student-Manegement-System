using StudenProject.DataModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudenProject.DomainModels
{
    public class PaymentHistorys
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int? InstallmentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string? RecordStatus { get; set; }
        public string? PaymentType { get; set; }

        [ForeignKey("PeriodId")]
        public Period Period { get; set; }

        public Installment Installment { get; set; }
    }
}
