using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Filters
{
    public class ValidateCourseExists : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateCourseExists(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository; _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT")!;

            var id = (string)context.ActionArguments[context.ActionArguments.Keys.Where(x => x.Equals("courseID")).SingleOrDefault()];

            var course = await _repository.Course.GetCourseById(new Guid(id), trackChanges);
            if (course is null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                var response = new ObjectResult(new ResponseModel
                {
                    StatusCode = 404,
                    Message = $"Course with id: {id} doesn't exist in the database."
                });
                context.Result = response;
            }
            else
            {
                context.HttpContext.Items.Add("course", course);
                await next();
            }
        }
    }
}