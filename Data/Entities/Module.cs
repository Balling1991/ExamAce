using ExamAce.Data.Enums;
using System;

namespace ExamAce.Data.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public ModuleStatus Status { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
