using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExamAce.Data.Entities;

namespace ExamAce.Data
{
    public interface IExamAceRepository
    {
        IEnumerable<Student> GetAllGrades();
        List<Student> GetStudentsInClassById(int id);
        Student GetStudentById(int id);

        void AddEntity(object model);
        bool SaveAll();
        
    }
}