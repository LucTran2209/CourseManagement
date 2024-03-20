using AutoMapper;
using CMS_API.Dtos.InputDto;
using CMS_API.Dtos.OutputDto;
using CMS_API.Models;

namespace CMS_API.Mappers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserOutputDto>()
                 .ForMember(o => o.Role, opt => opt.MapFrom(src => src.Role.RoleTitle));

            CreateMap<EnrrolCourseInputDto, UserJoinCourse>();



            CreateMap<AddContentCourseInputDto, CourseContent>();

            CreateMap<CourseContent, CoursesContentListOutputDto>();

            CreateMap<CreateNewCourse, Course>();

            CreateMap<CreateNewCourse, UserJoinCourse>()
                 .ForMember(o => o.TimeJoin, opt => opt.MapFrom(src => src.TimeStart));


        }
    }
}
