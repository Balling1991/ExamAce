using System.Collections.ObjectModel;

namespace ExamAce.Data.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public virtual Collection<Student> Students { get; set; }
    }
}
