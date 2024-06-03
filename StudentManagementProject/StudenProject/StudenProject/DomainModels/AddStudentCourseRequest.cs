namespace StudenProject.DomainModels
{
    public class AddStudentCourseRequest
    {
        public string StudentName { get; set; }
        public List<string> CourseNames { get; set; }
    }
}
