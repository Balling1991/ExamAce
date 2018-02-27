using ExamAce.Data.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ExamAce.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public Class Class { get; set; }
        public Collection<Grade> Grades { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
