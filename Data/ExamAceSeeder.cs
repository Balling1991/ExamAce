using AutoMapper;
using ExamAce.Data.Entities;
using ExamAce.Data.Enums;
using ExamAce.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ExamAce.Data
{
    public class ExamAceSeeder
    {
        private readonly ExamAceContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ExamAceSeeder(ExamAceContext ctx, IHostingEnvironment hosting, UserManager<User> userManager, IMapper mapper)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _hosting = hosting ?? throw new ArgumentNullException(nameof(hosting));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            // Ny karakter
            var newGradeViewModel = new GradeViewModel()
            {
                Name = GradeRange.MinusThree
            };

            // Mapping fra GradeViewModel til Grade
            var newGrade = _mapper.Map<GradeViewModel, Grade>(newGradeViewModel);

            // Ny bruger
            var newUserViewModel = new UserViewModel()
            {
                UserName = "homo@kaj.com",
                Email = "homo@kaj.com",
                FirstName = "Homo",
                LastName = "Kaj",
            };

            // Mapping fra UserViewModel til User
            var newUser = _mapper.Map<UserViewModel, User>(newUserViewModel);

            // Password til ny bruger
            var newPassword = "H0m0k@j!";
            //var newPassword = "N1c0lasErB0gs3!";

            var testStudentUser = await _userManager.FindByEmailAsync("homo@kaj.com");
            if (testStudentUser == null)
            {
                // Oprettelse af ny bruger
                var createNewUser = await _userManager.CreateAsync(newUser, newPassword);
                if (createNewUser.Succeeded)
                {
                    // Tilføj bruger til rolle
                    await _userManager.AddToRoleAsync(newUser, Roles.Student.ToString());
                }
            }

            // Ny klasse
            var newClassViewModel = new ClassViewModel()
            {
                Name = "3b",
                Year = 2018,
                Students = new Collection<Student>()
            };

            // Mapping fra ClassViewModel til Class 
            var newClass = _mapper.Map<ClassViewModel, Class>(newClassViewModel);

            // Ny student
            var newStudentViewModel = new StudentViewModel()
            {
                Grades = new Collection<Grade>(),
                Class = newClass,
                User = newUser
            };

            // Mapping fra StudentViewModel til Student
            var newStudent = _mapper.Map<StudentViewModel, Student>(newStudentViewModel);

            // Tilføj karakter til student
            newStudent.Grades.Add(newGrade);

            // Tilføj student til klasse
            newClass.Students.Add(newStudent);

            if (!_ctx.Grades.Any())
            {
                _ctx.Grades.Add(newGrade);
            }
            if (!_ctx.Classes.Any())
            {
                _ctx.Classes.Add(newClass);
            }
            if (!_ctx.Students.Any())
            {
                _ctx.Students.Add(newStudent);
            }
            _ctx.SaveChanges();
        }
    }
}
