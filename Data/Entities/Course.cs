using System.Collections.ObjectModel;

namespace ExamAce.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Collection<CourseTeacher> CourseTeacher { get; set; }
        public virtual Collection<Student> Students { get; set; }
        public virtual Collection<Assignment> Assignments { get; set; }
        public virtual Collection<Module> Modules { get; set; }
    }
}
