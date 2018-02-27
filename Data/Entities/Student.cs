using System.Collections.ObjectModel;

namespace ExamAce.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public Class Class { get; set; }
        public virtual Collection<Grade> Grades { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
