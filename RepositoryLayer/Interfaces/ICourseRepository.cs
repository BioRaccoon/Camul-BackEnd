using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCourses(bool trackChanges);
        Task<Course> GetCourseById(Guid courseID, bool trackChanges);
        Task<IEnumerable<Course>> GetCoursesWithPointId(Guid pointID, bool trackChanges);
        Task AddCourse(Course course);
        Task RemoveCourse(Course course);
    }
}
