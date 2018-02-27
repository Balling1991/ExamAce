using System.Collections.ObjectModel;

namespace ExamAce.Data.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public User User { get; set; }
        public virtual Collection<CourseTeacher> CourseTeacher { get; set; }
    }
}
