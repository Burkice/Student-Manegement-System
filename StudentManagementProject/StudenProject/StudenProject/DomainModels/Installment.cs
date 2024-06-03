namespace StudenProject.DataModels
{
    public class Installments
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int InstallmentC { get; set; } //taksit sayısı
        public string? Installmentt { get; set; }//ödeme tipi
        public string? PaymentStatus { get; set; }

   

        public int StudentId { get; set; }

        public Student Student { get; set; }
        public Period Period { get; set; }
    }
}
