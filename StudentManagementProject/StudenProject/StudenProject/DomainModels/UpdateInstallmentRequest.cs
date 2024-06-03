namespace StudenProject.DomainModels
{
    public class UpdateInstallmentRequest
    {
        public int InstallmentC { get; set; } //taksit sayısı
        public string? Installmentt { get; set; }//ödeme tipi
        public string? PaymentStatus { get; set; }


      
    }
}
