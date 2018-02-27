using ExamAce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamAce.Data
{
    public class ExamAceRepository : IExamAceRepository
    {
        private readonly ExamAceContext _ctx;
        private readonly ILogger<ExamAceRepository> _logger;

        public ExamAceRepository(ExamAceContext ctx, ILogger<ExamAceRepository> logger)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Student> GetAllGrades()
        {
            throw new NotImplementedException();
        }

        public Student GetStudentById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudentsInClassById(int id)
        {
            var student = _ctx.Students
                .Include(p => p.Class)
                .FirstOrDefault(o => o.UserId == id);

            return _ctx.Classes
                .SelectMany(o => o.Students)
                .Include(s => s.Class)
                .Include(s => s.User)
                .Where(p => p.Class.Id == student.Class.Id).ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
