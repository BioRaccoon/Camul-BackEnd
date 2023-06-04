using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ICoursesService
    {
        Task<CourseDto> CreateCourse(CourseCreationDto course);
        Task<IEnumerable<CourseDto>> GetAllCourses();
        Task<CourseDto> GetCourseById(Guid guid, bool trackChanges);
        Task RemoveCourse(CourseDto courseDto);
        Task UpdateCourse(CourseCreationDto course, Course? courseData);
        Task<IEnumerable<Point>> GetShortestPathToAM(Guid guid);
    }
}
