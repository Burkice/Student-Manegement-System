namespace StudenProject.DomainModels
{
    public class UpdateStudentRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Gender { get; set; }

        public string? PhysicalAddress { get; set; }
        public string? PostalAddress { get; set; }
    }
}
