using AutoMapper;
using CollegeApp.Data;
using CollegeApp.DTO;

namespace CollegeApp.Configuration
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<StudentDto, Student>().ReverseMap();
        }
    }
}
