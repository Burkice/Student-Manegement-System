namespace StudenProject.DomainModels
{
    public class PaymentUpdateRequest
    {

        public string? PaymentType { get; set; }
        public int? InstallmentCount { get; set; }
    }
}
