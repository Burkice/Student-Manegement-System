namespace StudenProject.DataModels
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public int PeriodId { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Period Period { get; set; }

        public List<StudentCourse> StudentCourse { get; set; }

        public Course()
        {
            StudentCourse = new List<StudentCourse>();
        }
    }
}
