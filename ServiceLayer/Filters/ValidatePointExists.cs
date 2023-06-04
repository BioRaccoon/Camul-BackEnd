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
    public class ValidatePointExists : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidatePointExists(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository; _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT")!;

            var id = (string)context.ActionArguments[context.ActionArguments.Keys.Where(x => x.Equals("pointID") || x.Equals("pointHttpId")).SingleOrDefault()];

            var point = await _repository.Point.GetPointById(new Guid(id), trackChanges);
            if (point is null)
            {
                _logger.LogInfo($"Point with id: {id} doesn't exist in the database.");
                var response = new ObjectResult(new ResponseModel
                {
                    StatusCode = 404,
                    Message = $"Point with id: {id} doesn't exist in the database."
                });
                context.Result = response;
            }
            else
            {
                context.HttpContext.Items.Add("point", point);
                await next();
            }
        }
    }
}