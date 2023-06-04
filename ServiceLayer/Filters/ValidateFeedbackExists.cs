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
    public class ValidateFeedbackExists : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateFeedbackExists(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository; _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT")!;

            var id = (string)context.ActionArguments[context.ActionArguments.Keys.Where(x => x.Equals("feedbackID")).SingleOrDefault()];

            var feedback = await _repository.Feedback.GetFeedbackById(new Guid(id), trackChanges);
            if (feedback is null)
            {
                _logger.LogInfo($"Feedback with id: {id} doesn't exist in the database.");
                var response = new ObjectResult(new ResponseModel
                {
                    StatusCode = 404,
                    Message = $"Feedback with id: {id} doesn't exist in the database."
                });
                context.Result = response;
            }
            else
            {
                context.HttpContext.Items.Add("gpsCoordinates", feedback);
                await next();
            }
        }
    }
}