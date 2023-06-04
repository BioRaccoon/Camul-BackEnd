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
    public class ValidateClientExists : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateClientExists(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository; _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT")!;

            var id = (string)context.ActionArguments[context.ActionArguments.Keys.Where(x => x.Equals("userID")).SingleOrDefault()];

            var client = await _repository.Client.GetClientById(new Guid(id), trackChanges);
            if (client is null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                var response = new ObjectResult(new ResponseModel
                {
                    StatusCode = 404,
                    Message = $"Client with id: {id} doesn't exist in the database."
                });
                context.Result = response;
            }
            else
            {
                context.HttpContext.Items.Add("client", client);
                await next();
            }
        }
    }
}