using ExamAce.Data.Entities;
using ExamAce.Data.Enums;
using System;

namespace ExamAce.Models
{
    public class ModuleViewModel
    {
        public int ModuleId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public ModuleStatus Status { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
