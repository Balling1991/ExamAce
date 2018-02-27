using AutoMapper;
using ExamAce.Data.Entities;
using ExamAce.Models;

namespace ExamAce.Data
{
    public class ExamAceMappingProfile : Profile
    {
        public ExamAceMappingProfile()
        {
            CreateMap<Grade, GradeViewModel>()
                .ForMember(o => o.GradeId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<Class, ClassViewModel>()
                .ForMember(o => o.ClassId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<User, UserViewModel>()
                .ForMember(o => o.UserId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<Student, StudentViewModel>()
                .ForMember(o => o.StudentId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<Teacher, TeacherViewModel>()
                .ForMember(o => o.TeacherId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();
        }
    }
}
