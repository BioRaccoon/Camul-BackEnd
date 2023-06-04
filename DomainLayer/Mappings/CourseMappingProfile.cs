using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Mappings
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();

            CreateMap<CourseCreationDto, Course>().ReverseMap();

            CreateMap<CourseUpdateDto, Course>().ReverseMap();
        }
    }
}