using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.GenericRepository.Service;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Course>> GetAllCourses(bool trackChanges)
        {
            return await FindAllAsync(trackChanges).Result.OrderBy(c => c.CourseID.ToString()).ToListAsync();
        }

        public async Task<Course?> GetCourseById(Guid courseID, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.CourseID.Equals(courseID), trackChanges).Result.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesWithPointId(Guid pointID, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.InitialPointID.Equals(pointID) || c.EndPointID.Equals(pointID), trackChanges).Result.ToListAsync();
        }

        public async Task AddCourse(Course course) => await CreateAsync(course);

        public async Task RemoveCourse(Course course) => await RemoveAsync(course);

    }
}