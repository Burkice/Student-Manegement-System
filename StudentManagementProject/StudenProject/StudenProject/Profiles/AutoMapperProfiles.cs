using AutoMapper;
using StudenProject.DataModels;
using StudenProject.DomainModels;
using StudenProject.Profiles.AfterMap;

namespace StudenProject.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student,Students>().ReverseMap();
            CreateMap<DataModels.Address, Addresss>().ReverseMap();
            CreateMap<DataModels.Course, Courses>().ReverseMap();
            CreateMap<DataModels.Discount, Discounts>().ReverseMap();
            CreateMap<DataModels.Installment, Installments>().ReverseMap();
            CreateMap<DataModels.PaymentHistory, PaymentHistorys>().ReverseMap();
            CreateMap<DataModels.Period, Periods>().ReverseMap();
            CreateMap<DataModels.Role, Roles>().ReverseMap();
            CreateMap<DataModels.StudentCourse, StudentCourses>().ReverseMap();
            CreateMap<DataModels.Teacher, Teachers>().ReverseMap();
            CreateMap<DataModels.User, Users>().ReverseMap();
            CreateMap<DataModels.Payment, Payments>().ReverseMap();
            CreateMap<UpdateStudentRequest, DataModels.Student>().AfterMap<UpdateStudentRequestAfterMap>();
            CreateMap<AddStudentRequest, DataModels.Student>().AfterMap<AddStudentRequestAfterMap>();
            CreateMap<Student, StudentCourse>();
            CreateMap<UpdateInstallmentRequest, DataModels.Installment>();
            CreateMap<PaymentUpdateRequest, DataModels.Payment>();
            CreateMap<StudentCourse, StudentCourseDTO>()
               .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Firstname + " " + src.Student.Lastname))
               .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));





        }
    }
}
