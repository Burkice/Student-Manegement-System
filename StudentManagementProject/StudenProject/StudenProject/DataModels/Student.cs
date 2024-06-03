namespace StudenProject.DataModels
{
    public class Student
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? Gender { get; set; }

        public Address Address { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
     

        public Student()
        {
            StudentCourses = new List<StudentCourse>();
        }
    }
}
