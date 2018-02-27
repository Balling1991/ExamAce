using ExamAce.Data.Entities;
using System.Collections.ObjectModel;

namespace ExamAce.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Collection<CourseTeacher> CourseTeacher { get; set; }
        public Collection<Student> Students { get; set; }
        public Collection<Assignment> Assignments { get; set; }
        public Collection<Module> Modules { get; set; }
    }
}
