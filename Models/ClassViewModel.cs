using ExamAce.Data.Entities;
using System.Collections.ObjectModel;

namespace ExamAce.Models
{
    public class ClassViewModel
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Collection<Student> Students { get; set; }
    }
}
