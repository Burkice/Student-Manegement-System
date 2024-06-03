using StudenProject.DataModels;

namespace StudenProject.DomainModels
{
    public class Discounts
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public int Rate { get; set; }

        public Student Student { get; set; }
    }
}
