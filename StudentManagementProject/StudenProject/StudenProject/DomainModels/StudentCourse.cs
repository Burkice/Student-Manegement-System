namespace StudenProject.DomainModels
{
    public class StudentCourses
    {
        public int StudentCourseId { get; set; }
        public int StudentId { get; set; }
        public Students Student { get; set; }
        public int CourseId { get; set; }
        public Courses Course { get; set; }


        public StudentCourses()
        {

        }
    }
}
