using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;

namespace IsepInDoorMapping.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : BaseApiController
    {
        private readonly ICoursesService _coursesService;
        public CoursesController(ILoggerManager logger,ICoursesService coursesService) : base(logger)
        {
            this._coursesService = coursesService;
        }

        /// <summary>
        /// Get all Courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var coursesDtos = await _coursesService.GetAllCourses();
                return Ok(coursesDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllCourses)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
            }
        }

        [HttpGet("shortestPath/{locationID}", Name = "ShortestPath")]
        public async Task<IActionResult> GetShortestPath(string locationID)
        {
            var shortestPathDto = await _coursesService.GetShortestPathToAM(new Guid(locationID));

            if (shortestPathDto is null)
            {
                _logger.LogInfo($"It wasn't possible to calculate the shortest path to Auditório Magno.");
                return NotFound();
            }
            else
            {
                return Ok(shortestPathDto);
            }
        }

        /// <summary>
        /// Get an Course by Id
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        [HttpGet("{courseID}", Name = "CourseById")]
        public async Task<IActionResult> GetCourseById(string courseID)
        {
            var courseDto = await _coursesService.GetCourseById(new Guid(courseID), trackChanges: false);

            if (courseDto is null)
            {
                _logger.LogInfo($"Course with id: {courseID} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(courseDto);
            }
        }

        /// <summary>
        /// Add a new Course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCourse([FromBody] CourseCreationDto course)
        {
            var courseReturn = await _coursesService.CreateCourse(course); 

            return CreatedAtRoute("CourseById",
                new
                {
                    courseId = courseReturn.CourseID
                },
                courseReturn);
        }

        /// <summary>
        /// Update an Course
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut("{courseID}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCourseExists))]
        public async Task<IActionResult> UpdateCourse(string courseID, [FromBody] CourseCreationDto course)
        {
            var courseData = HttpContext.Items["course"] as Course;

            await _coursesService.UpdateCourse(course, courseData);

            return NoContent();
        }

        /// <summary>
        /// Remove an Course
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        [HttpDelete("{courseID}")]
        public async Task<IActionResult> RemoveCourse(string courseID)
        {
            var courseDto = await _coursesService.GetCourseById(new Guid(courseID), trackChanges: false);

            if (courseDto is null)
            {
                _logger.LogInfo($"Course with id: {courseID} doesn't exist in the database. No need to delete it.");
                return NotFound();
            }
            else
            {
                await _coursesService.RemoveCourse(courseDto);
                return Ok();
            }
        }

    }
}