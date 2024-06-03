namespace StudenProject.DomainModels
{
    public class Addresss
    {
        public int Id { get; set; }
        public string? PhysicalAddress { get; set; }
        public string? PostalAddress { get; set; }
        public int StudentId { get; set; }
    }
}
