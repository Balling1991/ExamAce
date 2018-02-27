using ExamAce.Data.Entities;
using System.Collections.ObjectModel;

namespace ExamAce.Models
{
    public class TeacherViewModel
    {
        public int TeacherId { get; set; }
        public User User { get; set; }
        public Collection<CourseTeacher> CourseTeacher { get; set; }
    }
}
