using ExamAce.Data.Enums;
using System;
using System.Collections.ObjectModel;

namespace ExamAce.Data.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int StudentTime { get; set; }
        public DateTime Submitted { get; set; }
        public AssignmentStatus Status { get; set; }
        public AssignmentAwait Await { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Collection<Comment> Comments { get; set; }   
    }
}
